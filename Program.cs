using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Hosting;

namespace Sample01
{   
    class Program
    {
        [ImportMany]
        private ICarContract[] CarParts { get; set; }
      
        static void Main(string[] args)
        {
            new Program().Run();
        }

        void Run()
        {
            var container = new CompositionContainer();
            var batch = new CompositionBatch();
            ComposablePart partHost = batch.AddPart(this);
            ComposablePart partBMW = batch.AddPart(new BMW());
            ComposablePart partMercedes = batch.AddPart(new Mercedes());
            container.Compose(batch);

            foreach (ICarContract carPart in CarParts)
                Console.WriteLine(carPart.GetName());

            container.Dispose();
        }
    }

    public interface ICarContract
    {
        string GetName();
    }

    [Export(typeof(ICarContract))]
    public class Mercedes : ICarContract
    {
        public string GetName()
        {
            return "Mercedes";
        }
    }

    public class BMW : ICarContract
    {
        public string GetName()
        {
            return "BMW";
        }
    }
}
