using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    public   class Best_Menu :Best_tipos_articulos // calzado, indumentaria
    { 
      
        List<Best_tipos_categorias> _TipoCat = new List<Best_tipos_categorias>(); // genero, MARCA
        public List<Best_tipos_categorias> TipoCat
        {
            get { return _TipoCat; }
            set { _TipoCat = value; }

        }



        public Best_categorias buscarCategoria(string catids)
        {
            Best_categorias sal = new Best_categorias();
            foreach (var item in _TipoCat)
            {

               sal = item.buscarCategoria(catids);

                if (sal.Nombre != null) {
                    break;
                }
               
            }
            return sal;

        }




    }


 


}

