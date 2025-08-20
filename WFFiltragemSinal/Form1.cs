using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace WFFiltragemSinal
{
    public partial class Form1 : Form
    {
        private float sampleInterval = 0;  // ms
        private float[] data;

        public Form1()
        {
            InitializeComponent();

            buttonSave.Enabled = false;
        }

        private string initialDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName;

        public void Plot() { 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                InitialDirectory = initialDirectory,
                Filter = "JSON Files (*.json)|*.json"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                string content = File.ReadAllText(path);
                SeismicData data = JsonConvert.DeserializeObject<SeismicData>(content);

                if (data.interval == 0)
                {
                    MessageBox.Show($"Could not load ${path}. Missing interval.");
                    return;
                }

                if (data.data == null)
                {
                    MessageBox.Show($"Could not load ${path}. Missing data.");
                    return;
                }

                if (data.name == null)
                    data.name = "Plot";

                this.data = data.data;
                sampleInterval = data.interval;

                seismicPlot.DrawPlot(this.data, sampleInterval, data.name);

                if (data.modifiedData != null) 
                    seismicPlot.UpdateModifiedPlot(data.modifiedData, data.highPassFrequencyCut, data.lowPassFrequencyCut);

                buttonSave.Enabled = true;

            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                InitialDirectory = initialDirectory,
                Filter = "JSON Files (*.json)|*.json"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                SeismicData data = new SeismicData
                {
                    name = seismicPlot.PlotName,
                    interval = seismicPlot.SampleInterval,
                    data = seismicPlot.Data,
                    modifiedData = seismicPlot.ModifiedData,
                    highPassFrequencyCut = seismicPlot.HighPassFrequency,
                    lowPassFrequencyCut = seismicPlot.LowPassFrequency
                };
                string content = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(path, content);
                MessageBox.Show("Save Successful");
            }
        }

    }
}
