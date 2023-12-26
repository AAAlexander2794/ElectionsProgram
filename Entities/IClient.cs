using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    public interface IClient
    {
        public string Name { get; }

        public List<Talon> Talons { get; }
    }
}
