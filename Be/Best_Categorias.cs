using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    public class Best_categorias
    {
        public string Id { get; set; }
        public string Nombre { get; set; }

        public string Tipo_categoria_id { get; set; }

        public string tipo_articulo_id { get; set; }
    }


    public class Best_tipos_categorias
    {
        public string Id { get; set; }
        public string Nombre { get; set; }

        List<Best_categorias> _Cat = new List<Best_categorias>(); // hombre, mujer
        public List<Best_categorias> Categoria
        {
            get { return _Cat; }
            set { _Cat = value; }

        } 


        public Best_categorias buscarCategoria( string catids)
        {
            Best_categorias sal = new Best_categorias();
            foreach (var item in Categoria)
            {
                if (item.Id == catids)
                {

                    sal = item;
                    break;
                }
            }
            return sal;

        }
    }




}
