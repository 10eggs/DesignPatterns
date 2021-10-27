using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.SOLID.InterfacesSegregation
{
    public class Document
    {
    }

    //You can not implement this interface to all printers. There are printers with printing/scanning options only
    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }
    public interface IPrinter { void Print(Document d); }
    public interface IScanner { void Scan(Document d); }
    public interface IFax { void Fax(Document d); }
    public interface IMultiFunctionDevice : IScanner, IPrinter { }

    public class MultiFunctionMachine : IMultiFunctionDevice
    {
        private IPrinter printer;
        private IScanner scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            if(printer == null)
            {
                throw new ArgumentNullException(paramName: nameof(printer));
            }
            if(scanner == null)
            {
                throw new ArgumentNullException(paramName: nameof(scanner));
            }

            this.printer = printer;
            this.scanner = scanner;  

        }
        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d);
        }
    }

}
