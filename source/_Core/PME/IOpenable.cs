using System;

namespace PME
{
    public interface IOpenable // Yes, it's a word now...
    {
        bool IsOpen
        {
            get;
        }

        void Open();

        void Close();
    }
}

