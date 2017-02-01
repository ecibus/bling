using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bling.Domain.Secondary;
using Bling.Domain.Extension;

namespace Bling.Presenter.Secondary
{
    public interface ICountryPlaceFormView
    {
        string SourceFileName { get; }
        string TargetPath { get; }
        string CountryPlaceData { set; }
    }

    public class CountryPlaceFormPresenter : Presenter
    {
        private ICountryPlaceFormView m_View;

        public CountryPlaceFormPresenter(ICountryPlaceFormView view)
        {
            m_View = view;
        }

        public void LoadData()
        {
            List<CountryPlaceOptimalBlue> ob = new List<CountryPlaceOptimalBlue>();

            using (TextReader reader = File.OpenText(m_View.SourceFileName))
            {
                while (reader.Peek() != -1)
                {
                    string[] line = reader.ReadLine().Split(',');

                    if (line[0] == "GOVERNMENT" || line[0] == "GOVERNMENT NO FEE")
                    {
                        reader.ReadLine();
                        reader.ReadLine();
                        reader.ReadLine();

                        string[] rate = reader.ReadLine().Split(',');

                        while (rate[0] != "")
                        {
                            ob.Add(new CountryPlaceOptimalBlue("FHA30", rate[0], rate[1], "30"));
                            ob.Add(new CountryPlaceOptimalBlue("FHA30", rate[0], rate[2], "60"));
                            ob.Add(new CountryPlaceOptimalBlue("FHA30", rate[0], rate[3], "90"));

                            ob.Add(new CountryPlaceOptimalBlue("FHA15", rate[5], rate[6], "30"));
                            ob.Add(new CountryPlaceOptimalBlue("FHA15", rate[5], rate[7], "60"));
                            ob.Add(new CountryPlaceOptimalBlue("FHA15", rate[5], rate[8], "90"));

                            rate = reader.ReadLine().Split(',');

                        }

                    }

                    if (line[0] == "CONVENTIONAL" || line[0] == "CONVENTIONAL NO FEE")
                    {
                        reader.ReadLine();
                        reader.ReadLine();
                        reader.ReadLine();

                        string[] rate = reader.ReadLine().Split(',');

                        while (rate[0] != "")
                        {
                            ob.Add(new CountryPlaceOptimalBlue("CO30", rate[0], rate[1], "30"));
                            ob.Add(new CountryPlaceOptimalBlue("CO30", rate[0], rate[2], "60"));
                            ob.Add(new CountryPlaceOptimalBlue("CO30", rate[0], rate[3], "90"));

                            ob.Add(new CountryPlaceOptimalBlue("CO15", rate[5], rate[6], "30"));
                            ob.Add(new CountryPlaceOptimalBlue("CO15", rate[5], rate[7], "60"));
                            ob.Add(new CountryPlaceOptimalBlue("CO15", rate[5], rate[8], "90"));

                            rate = reader.ReadLine().Split(',');

                        }

                    }

                }
            }

            // Create flat file
            using (TextWriter writer = File.CreateText(m_View.TargetPath + "\\" + "OB.csv"))
            {
                writer.WriteLine("Product,Rate,Price,Lock");

                foreach (var o in ob)
                {
                    if (o.Price == "N/A")
                        continue;

                    writer.WriteLine(String.Format("{0},{1},{2},{3}", o.Product, o.Rate, o.Price, o.Lock));
                }
            }

            m_View.CountryPlaceData = "Click this <a href='Report/OB.csv'>link</a> to get the flat file.";
        }

    }
}
