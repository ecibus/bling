using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.HR
{
    public interface ITimeCardDao : IDao<string, int>
    {
        string GetTimeCard(bool accepted, int month, int year);
    }

    public class TimeCardDao : AbstractDao<string, int>, ITimeCardDao
    {
        public TimeCardDao(ISession session)
            : base (session)
        {
        }

        public string GetTimeCard(bool accepted, int month, int year)
        {

            StringBuilder json = new StringBuilder();

            json.Append("[");

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_TimeCard_GetAllByAccepted";
                    cmd.Parameters.AddWithValue("accepted", accepted);
                    cmd.Parameters.AddWithValue("month", month);
                    cmd.Parameters.AddWithValue("year", year);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        json.AppendFormat(
                            "{{ " +
                            "\"Name\" : \"{0}\", \"TimeCardSubmitId\" : \"{94}\", \"IsAccepted\" : \"{95}\", \"IsSubmitted\" : \"{96}\", " +
                            "\"Reg\" : [ {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31} ], " +
                            "\"Ovt\" : [ {32}, {33}, {34}, {35}, {36}, {37}, {38}, {39}, {40}, {41}, {42}, {43}, {44}, {45}, {46}, {47}, {48}, {49}, {50}, {51}, {52}, {53}, {54}, {55}, {56}, {57}, {58}, {59}, {60}, {61}, {62} ], " +
                            "\"Dbl\" : [ {63}, {64}, {65}, {66}, {67}, {68}, {69}, {70}, {71}, {72}, {73}, {74}, {75}, {76}, {77}, {78}, {79}, {80}, {81}, {82}, {83}, {84}, {85}, {86}, {87}, {88}, {89}, {90}, {91}, {92}, {93} ] " +
                            "}},",
                            reader["Full Name"],
                            reader["Reg1"], reader["Reg2"], reader["Reg3"], reader["Reg4"], reader["Reg5"], reader["Reg6"], reader["Reg7"], reader["Reg8"], reader["Reg9"], reader["Reg10"],
                            reader["Reg11"], reader["Reg12"], reader["Reg13"], reader["Reg14"], reader["Reg15"], reader["Reg16"], reader["Reg17"], reader["Reg18"], reader["Reg19"], reader["Reg20"],
                            reader["Reg21"], reader["Reg22"], reader["Reg23"], reader["Reg24"], reader["Reg25"], reader["Reg26"], reader["Reg27"], reader["Reg28"], reader["Reg29"], reader["Reg30"],
                            reader["Reg31"],
                            reader["Ovt1"], reader["Ovt2"], reader["Ovt3"], reader["Ovt4"], reader["Ovt5"], reader["Ovt6"], reader["Ovt7"], reader["Ovt8"], reader["Ovt9"], reader["Ovt10"],
                            reader["Ovt11"], reader["Ovt12"], reader["Ovt13"], reader["Ovt14"], reader["Ovt15"], reader["Ovt16"], reader["Ovt17"], reader["Ovt18"], reader["Ovt19"], reader["Ovt20"],
                            reader["Ovt21"], reader["Ovt22"], reader["Ovt23"], reader["Ovt24"], reader["Ovt25"], reader["Ovt26"], reader["Ovt27"], reader["Ovt28"], reader["Ovt29"], reader["Ovt30"],
                            reader["Ovt31"],
                            reader["Dbl1"], reader["Dbl2"], reader["Dbl3"], reader["Dbl4"], reader["Dbl5"], reader["Dbl6"], reader["Dbl7"], reader["Dbl8"], reader["Dbl9"], reader["Dbl10"],
                            reader["Dbl11"], reader["Dbl12"], reader["Dbl13"], reader["Dbl14"], reader["Dbl15"], reader["Dbl16"], reader["Dbl17"], reader["Dbl18"], reader["Dbl19"], reader["Dbl20"],
                            reader["Dbl21"], reader["Dbl22"], reader["Dbl23"], reader["Dbl24"], reader["Dbl25"], reader["Dbl26"], reader["Dbl27"], reader["Dbl28"], reader["Dbl29"], reader["Dbl30"],
                            reader["Dbl31"],
                            reader["TimeCardSubmitId"],
                            reader["IsAccepted"],
                            reader["IsSubmitted"]
                            );
                    }
                }
            }

            //"[ " +
            //" {  \"Name\" : \"Hello\", \"Hours\" : [1, 2, 3, 4, 5] }, " +
            //" {  \"Name\" : \"World\", \"Hours\" : [6, 7, 8, 9, 10] } " +
            //"]";

            if (json.Length > 1)
                json.Remove(json.Length - 1, 1);
            json.Append("]");

            return json.ToString();
        }
    }
}
