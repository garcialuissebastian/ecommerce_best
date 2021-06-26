using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Be;
namespace Dal
{
    interface IMapeadorPermiso : IMapeadorCRUD<PermisoBase, PermisoFiltro>
    {

        List<PermisoBase> ListarHijos(PermisoFiltro filtro);
        bool AgregarHijo(PermisoBase padre, PermisoBase hijo);
        bool QuitarHijo(PermisoBase padre, PermisoBase hijo);
        bool QuitarTodosHijos(PermisoBase padre);
    }

}
