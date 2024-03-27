using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Visitor_Example();


            GameObject gameObject = new GameObject();
            gameObject.AddComponent<Camera>();

            Camera cam = gameObject.GetComponent<Camera>();

            cam.Draw();
            Console.ReadLine();

        }

        public class Camera
        {
            public void Draw()
            {
                Console.WriteLine("Camera!");
            }
        }

        private static void Visitor_Example()
        {
            MagicBuilder builder = new MagicBuilder();

            MagicData data = new FireData();

            Magic magic = builder.Visit(data);

            Console.WriteLine(magic.GetType());
        }
    }


    public interface IMagicVisitor
    {
        Magic Visit(MagicData data);
    }

    public interface IMagicData
    {
        Magic Accept(IMagicVisitor visitor);
    }

    public class MagicBuilder : IMagicVisitor
    {

        public Magic Visit(MagicData data)
        {
            return data.Accept(this);
        }
    }

    public abstract class Magic
    {
        public Magic(MagicData data)
        {

        }


    }

    public abstract class MagicData : IMagicData
    {
        public abstract Magic Accept(IMagicVisitor visitor);
    }



    public class Fire : Magic
    {
        public Fire(FireData data) : base(data)
        {
        }
    }

    public class FireData : MagicData
    {
        public override Magic Accept(IMagicVisitor visitor)
        {
            return new Fire(this);
        }
    }

    public class Ice : Magic
    {
        public Ice(IceData data) : base(data)
        {

        }
    }

    public class IceData : MagicData
    {
        public override Magic Accept(IMagicVisitor visitor)
        {
            return new Ice(this);
        }
    }

}
