using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Audio.Filters;
using Accord.Audio;
using System.Runtime.InteropServices;
using System.Globalization;
using Accord.Audio.ComplexFilters;

namespace WFFiltragemSinal
{
    public partial class SeismicPlot : UserControl
    {
        public float[] Data { get; set; }
        public float[] ModifiedData { get; private set; }
        public float SampleInterval { get; set; }  // ms
        private float sampleRate;  // Hz
        private float nyquistRate;  // Hz

        public string PlotName { get; set; }

        public PlotModel Model { get; set; }

        private LineSeries seriesOriginal;
        private LineSeries seriesModified;
        public float HighPassFrequency { get; private set; }
        public float LowPassFrequency { get; private set; }

        private bool syncing = false;

        public SeismicPlot()
        {
            InitializeComponent();

            textBoxLowPass.KeyPress += (s, e) =>
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
            };
            textBoxHighPass.KeyPress += (s, e) =>
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
            };

            textBoxLowPass.TextChanged += (s, e) =>
            {
                FilterBoxUpdated();
            };
            textBoxHighPass.TextChanged += (s, e) =>
            {
                FilterBoxUpdated();
            };

            textBoxLowPass.Enabled = false;
            textBoxHighPass.Enabled = false;

            trackBarLowPass.ValueChanged += (s, e) =>
            {
                if (!syncing)
                    if (trackBarLowPass.Value == 0)
                        textBoxLowPass.Text = "";
                    else
                        textBoxLowPass.Text = trackBarLowPass.Value.ToString();
            };
            trackBarHighPass.ValueChanged += (s, e) =>
            {
                if (!syncing)
                    if (trackBarHighPass.Value == 0)
                        textBoxHighPass.Text = "";
                    else
                        textBoxHighPass.Text = trackBarHighPass.Value.ToString();
            };

            trackBarLowPass.Enabled = false;
            trackBarHighPass.Enabled = false;

        }
        private void FilterBoxUpdated() {
            float cut = 0;

            for (int z = 0; z < ModifiedData.Length; z++)
                ModifiedData[z] = (float)seriesOriginal.Points[z].X;

            if (float.TryParse(textBoxHighPass.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out cut))
            {
                HighPassFrequency = cut;
                if (HighPassFrequency >= nyquistRate)
                    HighPassFrequency = nyquistRate;
                ModifiedData = ApplyHighPassFilter(ModifiedData, HighPassFrequency, sampleRate);
                syncing = true;
                trackBarHighPass.Value = (int)HighPassFrequency;
                syncing = false;
            }

            if (float.TryParse(textBoxLowPass.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out cut))
            {
                LowPassFrequency = cut;
                if (LowPassFrequency >= nyquistRate)
                    LowPassFrequency = nyquistRate;
                ModifiedData = ApplyLowPassFilter(ModifiedData, LowPassFrequency, sampleRate);
                syncing = true;
                trackBarLowPass.Value = (int)LowPassFrequency;
                syncing = false;
            }

            for (int z = 0; z < ModifiedData.Length; z++)
                seriesModified.Points[z] = new DataPoint((float)ModifiedData[z], seriesModified.Points[z].Y);

            Model.InvalidatePlot(true);
        }
        private float[] ApplyLowPassFilter(float[] points, float cutFrequency, float sampleRate)
        {
            var lowFilter = new LowPassFilter(frequency: cutFrequency, sampleRate: sampleRate);
            Signal inSignal = Signal.FromArray(points, sampleRate: (int)sampleRate, channels: 1, format: SampleFormat.Format32BitIeeeFloat);
            Signal filteredSignal = lowFilter.Apply(inSignal);
            return filteredSignal.ToFloat();
        }

        private float[] ApplyHighPassFilter(float[] points, float cutFrequency, float sampleRate)
        {
            var highFilter = new HighPassFilter(frequency: cutFrequency, sampleRate: sampleRate);
            Signal inSignal = Signal.FromArray(points, sampleRate: (int)sampleRate, channels: 1, format: SampleFormat.Format32BitIeeeFloat);
            Signal filteredSignal = highFilter.Apply(inSignal);
            return filteredSignal.ToFloat();
        }


        public void DrawPlot(float[] data, float sampleInterval, string plotName="Plot")
        {
            PlotName = plotName;
            Data = data;
            ModifiedData = (float[]) Data.Clone();
            SampleInterval = sampleInterval;
            sampleRate = 1000 / SampleInterval;
            nyquistRate = sampleRate / 2;

            trackBarLowPass.Minimum = 0;
            trackBarLowPass.Maximum = (int)nyquistRate;
            trackBarLowPass.TickFrequency = 1;

            trackBarHighPass.Minimum = 0;
            trackBarHighPass.Maximum = (int)nyquistRate;
            trackBarHighPass.TickFrequency = 1;

            textBoxLowPass.Enabled = true;
            textBoxHighPass.Enabled = true;
            trackBarLowPass.Enabled = true;
            trackBarHighPass.Enabled = true;

            textBoxLowPass.Text = "";
            textBoxHighPass.Text = "";

            trackBarLowPass.Value = 0;
            trackBarHighPass.Value = 0;

            Model = new PlotModel 
            { 
                Title = PlotName
            };
            plotView.Model = Model;

            Model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Amplitude",
            });

            Model.Axes.Add(new LinearAxis
            { 
                Position = AxisPosition.Left,
                Title = "Time (ms)",
            });

            seriesOriginal = new LineSeries
            {
                Title = "Original Seismic",
                Color = OxyColors.Black
            };
            Model.Series.Add(seriesOriginal);

            seriesModified = new LineSeries
            {
                Title = "Modified Seismic",
                Color = OxyColors.Blue
            };
            Model.Series.Add(seriesModified);

            for (int s = 0; s < Data.Length; s++) {
                float time = s * SampleInterval;
                float amplitude = Data[s];
                seriesOriginal.Points.Add(new DataPoint((float)amplitude, time));
                seriesModified.Points.Add(new DataPoint((float)amplitude, time));
            }
        }

        public void UpdateModifiedPlot(float[] modifiedData, float highPassFrequency, float lowPassFrequency) 
        {
            ModifiedData = (float[]) modifiedData.Clone();
            // HighPassFrequency = highPassFrequency;
            // LowPassFrequency = lowPassFrequency;

            if (lowPassFrequency == 0)
                textBoxLowPass.Text = "";
            else
                textBoxLowPass.Text = lowPassFrequency.ToString(CultureInfo.InvariantCulture);

            if (highPassFrequency == 0)
                textBoxHighPass.Text = "";
            else
                textBoxHighPass.Text = highPassFrequency.ToString(CultureInfo.InvariantCulture);

            for (int z = 0; z < ModifiedData.Length; z++)
                seriesModified.Points[z] = new DataPoint((float)ModifiedData[z], seriesModified.Points[z].Y);
            Model.InvalidatePlot(true);
        }

        private void trackBarLowPass_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBarHighPass_Scroll(object sender, EventArgs e)
        {

        }

        private void SeismicPlot_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void plotView_Click(object sender, EventArgs e)
        {

        }
    }
}
