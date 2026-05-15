using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp10
{
    public class Documento 
    {
        /// <summary>
        /// Originador: Es el objeto que tiene un estado interno que puede cambiar con el tiempo. El Originador crea un Memento que contiene una instantánea de su estado actual y lo guarda fuera de sí mismo. También puede usar el Memento para restaurar su estado anterior.
        /// </summary>
        private string state;
        private string text;

        public string Text { get => text; set => text = value; }

        public string State { get => state; set => state = value; } 

        public Documento( string State) 
        {
            this.state = State;
        }

        public void SetMemento(IMemento memento)
        {
             this.state = memento.getState();
        }

        public IMemento CreateMemento()
        {
            return new Memento(this.state);
        }
    }
}
