using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be
{
    public class Best_articulos
    {
        public string id { get; set; }
        public string sucursal_id { get; set; }
        public string tipo { get; set; }
        public string codigo { get; set; }
        public string talle { get; set; }
        public string descripcion { get; set; }
        public string proveedor_id { get; set; }
        public string precio_historico { get; set; }
        public string precio_compra { get; set; }
        public string precio_venta { get; set; }
        public string utilidad { get; set; }
        public string fecha_lista { get; set; }
        public string fecha_compra { get; set; }
        public string fecha_baja { get; set; }
        public string estado { get; set; }
        public string codbar { get; set; }
        public string codbar_fab { get; set; }
        public DateTime aud_ing_fec { get; set; }
        public string tipo_articulo_id { get; set; }
        public string aud_ing_por { get; set; }
        public DateTime aud_mod_fec { get; set; }
        public string aud_mod_por { get; set; }
        public string disponibleWeb { get; set; }
        public string etiquetaWeb { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public string Imagen4 { get; set; }
        public string Imagen5 { get; set; }
  

    }

    public class Best_tipos_articulos
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }

    }

    public class Best_articulos_venta
    {
        public string Id { get; set; }
        public string codigo { get; set; }
        public string Descripcion { get; set; }
        public string tipo_articulo { get; set; }
        public string precio { get; set; }
        public string genero { get; set; }
        public string categoria { get; set; }
        public string diciplina { get; set; }
        public string marca { get; set; }
        public string etiquetaWeb { get; set; } 
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public string Imagen4 { get; set; }
        public string Imagen5 { get; set; }

        public string disponibleWeb { get; set; }
        public string alto_embalaje { get; set; }
        public string peso_embalaje { get; set; } 
        public string ancho_embalaje { get; set; }
        public string largo_embalaje { get; set; }
    }


    public class Best_articulos_ventaId: Best_articulos_venta
    {
        public string tipo_articulo_id { get; set; }
        public string clasificacion_id { get; set; }
        public string tipo_clasificacion_id { get; set; }

        public string VolumenTotal { get; set; }
        public string Peso { get; set; }

        public string alto_embalaje { get; set; }
        public string ancho_embalaje { get; set; }
        public string largo_embalaje { get; set; }

        List<Best_articulos_venta_stock> _talles = new List<Best_articulos_venta_stock>(); // genero, MARCA
        public List<Best_articulos_venta_stock> Talles
        {
            get { return _talles; }
            set { _talles = value; }

        } 

        public string Stock()
        {
            string sal = "";

            foreach (var item in Talles)
            {
                if (item.id_art == Id)
                {
                    sal =  item.stock;

                }
            }

            return sal ;
        }

        public string talle()
        {
            string sal = "";

            foreach (var item in Talles)
            {
                if (item.id_art == Id)
                {
                    sal = item.talle;

                }
            }

            return sal;
        }

    }

    public class Best_articulos_venta_stock 
    {
        public string id_art { get; set; }
        public string talle { get; set; }
        public string UK { get; set; }
        public string CM { get; set; }
        public string US { get; set; }
        public string ARG { get; set; }
        public string stock { get; set; }
        
    }

    public class Best_articulos_carrito
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public string imagen { get; set; }
        public string precio { get; set; }
        public string genero { get; set; }
        public string categoria { get; set; }
        public string diciplina { get; set; }
        public string marca { get; set; }

        public string cant { get; set; }
        public string VolumenTotal { get; set; }
        public string Peso { get; set; }

        public string alto_embalaje { get; set; }
        public string ancho_embalaje { get; set; }
        public string largo_embalaje { get; set; }
    }


    public class Best_articulos_admin
    {

        //List<Best_articulos_admin> _talles = new List<Best_articulos_admin>(); // genero, MARCA
        //public List<Best_articulos_admin> Lista_Best_articulos_admin
        //{
        //    get { return _talles; }
        //    set { _talles = value; }

        //}


        public string Id { get; set; }
        public string codigo { get; set; }
        public string talle { get; set; }
        public string precio { get; set; }
        public string stock { get; set; }
        public string descripcion { get; set; }
        
        public string tipo_articulo_id { get; set; }
        public string clasificacion_id { get; set; }
        public string tipo_clasificacion_id { get; set; }

        public string disponibleWeb { get; set; }
        public string etiquetaWeb { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; } 
        public string Imagen4 { get; set; }
        public string Imagen5 { get; set; }

        public string genero { get; set; }
        public string categoria { get; set; }
        public string diciplina { get; set; }
        public string marca { get; set; }


        List<Combos> Igen = new List<Combos>();
        public List<Combos> ListaGeneros
        {
            get { return Igen; }
            set { Igen = value; }

        }


        List<Combos> Idic = new List<Combos>();
        public List<Combos> ListaDiciplina
        {
            get { return Idic; }
            set { Idic = value; }

        }

    }

}
