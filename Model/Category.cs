using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Category
    {
        private int id;
        private int parent_id;
        private string name;
        private string description;
        private bool statu;

        public int Id { get => id; set => id = value; }
        public int Parent_id { get => parent_id; set => parent_id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public bool Statu { get => statu; set => statu = value; }
    }
}
