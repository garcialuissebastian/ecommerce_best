using Be;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Bll
{
  public  class bllFactura
    {
        public string insert(factura fe)
        {
            Dal.DalBest best = new Dal.DalBest();
            return best.Insert(fe);

        }

        public DataSet RpFc(string ids)
        {
            Dal.DalBest best = new Dal.DalBest();
            return best.RpFc(  ids);

        }
        public static int ivaId(string valor)
        {

            if (valor == "0%")
            {
                return 3;

            }
            if (valor == "21%")
            {
                return 5;

            }

            if (valor == "10.5%")
            {
                return 4;

            }

            if (valor == "27%")
            {
                return 6;

            }
            return 0;
        }



        public static string LlenarComprobante(string v_num)
        {
            string sal = "";

            char[] NumArray = v_num.ToCharArray();

            for (int i = 0; i < (8 - NumArray.Length); i++)
            {
                sal = sal + "0";

            }

            return sal + v_num;


        }


    
        public static string TipoCbteStr(string cmbTipoCbte)
        {
            string tipoC = "";
            if (cmbTipoCbte == "11")
            {
                tipoC = "Factura C";

            }
            if (cmbTipoCbte == "13")
            {
                tipoC = "Nota de crédito C";

            }


            if (cmbTipoCbte == "1")
            {
                tipoC = "Factura A";

            }

            if (cmbTipoCbte == "3")
            {
                tipoC = "Nota de Crédito A";

            }

            if (cmbTipoCbte == "2")
            {
                tipoC = "Nota de Débito A";

            }

            if (cmbTipoCbte == "6")
            {
                tipoC = "Factura B";

            }

            if (cmbTipoCbte == "8")
            {
                tipoC = "Nota de Crédito B";

            }

            if (cmbTipoCbte == "7")
            {
                tipoC = "Nota de Débito B";

            }

            return tipoC;
        }

        public static int Mod10(string str)
        {
            // segun rg1702 afip

            int bb = 0;
            Int32 impar = 0;
            Int32 par = 0;
            Int32 etapa2 = 0;
            Int32 etapa4 = 0;
            Int32 digito = 0;

            foreach (char c in str.ToCharArray())
            {

                // 1 paso sumo los q estan en posicion impar

                bb++;

                if (!(bb % 2 == 0))
                {
                    impar += Convert.ToInt32(c.ToString());
                }
                else
                {
                    par += Convert.ToInt32(c.ToString());

                }



                // multiplico el resultado por 3
                etapa2 = impar * 3;
                // sumo etap2 mas pares

                etapa4 = par + etapa2;


                digito = 10 - (etapa4 - ((etapa4 / 10) * 10));


            }
            if (digito == 10) { return 0; }
            else
            { return digito; }

        }


        public string PathReportCbte(string v_obj)
        {
            string ppr = "";
            try
            {

                if (v_obj == "6")
                {
                    ppr = "Sport.Views.report.CbateB.rdlc";
                }
                if (v_obj == "8")
                {
                    ppr = "Sport.Views.report.CbatenNcB.rdlc";
                }
                if (v_obj == "7")
                {
                    ppr = "Sport.Views.report.CbatenNdB.rdlc";
                }

                if (v_obj == "1")
                {
                    ppr = "Sport.Views.report.CbateA.rdlc";
                }
                if (v_obj == "3")
                {
                    ppr = "Sport.Views.report.CbateNcA.rdlc";
                }
                if (v_obj == "2")
                {
                    ppr = "Sport.Views.report.CbateNdA.rdlc";
                }
                if (v_obj == "11")
                {
                    ppr = "Sport.Views.report.CbateC.rdlc";

                }

                if (v_obj == "13")
                {
                    ppr = "Sport.Views.report.CbateNcC.rdlc";
                }


                return ppr;
            }
            catch (Exception)
            {
                throw;
            }

        }


        //public List<factura> Buscar(string v_tipo, string v_num)
        //{

        // //   return factura.Buscar(v_tipo, v_num);

        //}

    }
}
