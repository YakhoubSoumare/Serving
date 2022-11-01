using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Windows;

namespace Serving
{
    internal class AsyncCooking
    {

        public async void MatLagning()
        {
            Task.WaitAll(KokaPasta());
        }
        public string[] ReceptView()
        {
            string receptfil = "recept.txt";
            StreamReader sr = new StreamReader(receptfil);
            string text = sr.ReadToEnd();
            sr.Close();
            string[] receptText = text.Split('\n');
            return (receptText);
        }

        static async Task KokaPasta()
        {
            MessageBox.Show("Pastan är under värme!");

            await Task.Delay(3000);

            MessageBox.Show("Pastan är klar!");
        }
    }
}