using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Command : ICommand
    {
        private Action action;

        public Command(Action action)
        {
            this.action = action;
        }

        public override void Execute()
        {
            this.action.Invoke();
        }
    }
}
