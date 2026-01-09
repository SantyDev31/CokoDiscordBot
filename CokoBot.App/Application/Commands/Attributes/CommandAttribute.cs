using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.App.Application.Commands.Attributes
{
    public class CommandAttribute : Attribute
    {
        public string Name { get;}
        public CommandAttribute(string Name) {
            this.Name = Name; 
        }
    }
}
