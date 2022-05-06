using Data;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BCategory
    {
        public  static async Task<DataTable> List()
        {
            DCategory Datos = new DCategory();
            return await Datos.List();
        }

        public static DataTable Search(string Valor)
        {
            DCategory Datos = new DCategory();
            return Datos.Search(Valor);
        }

        public static async Task<string> Insert(int parent_id,string Name, string Description)
        {
            DCategory Datos = new DCategory();

            string Existe =await Datos.Exist(Name);
            if (Existe.Equals("1"))
            {
                return "La categoría ya existe";
            }
            else if (Existe.Equals("0"))
            {
                Category category = new Category();
                category.Name = Name;
                category.Parent_id = parent_id;
                category.Description = Description;
                return await Datos.Insert(category);
            }
            else
            {
                return Existe;
            }
        }

        public static string Update(int Id,string NombreAnterior ,string Name, string Description)
        {
            DCategory Datos = new DCategory();

            Category category = new Category();

            if (NombreAnterior.Equals(Name))
            {
                category.Id = Id;
                category.Name = Name;
                category.Description = Description;
                return Datos.Update(category);
            }
            else
            {

                //string Existe = Datos.Exist(Name);
                //if (Existe.Equals("1"))
                //{
                //    return "La categoría ya existe";
                //}
                //else if (Existe.Equals("0"))
                //{
                //    //Category category = new Category();
                //    category.Id = Id;
                //    category.Name = Name;
                //    category.Description = Description;
                //    return Datos.Update(category);

                //}
                //else
                //{
                //    return Existe;
                //}
            }
            return null;



        }

        public static string Delete(int Id)
        {
            DCategory Datos = new DCategory();
            return Datos.Delete(Id);
        }

        public static string Active(int Id)
        {
            DCategory Datos = new DCategory();
            return Datos.Active(Id);
        }

        public static string Desactive(int Id)
        {
            DCategory Datos = new DCategory();
            return Datos.Desactive(Id);
        }
    }
}
