using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivero.Data;
using Vivero.Entities;

namespace Vivero.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            GetListTiposDePlantas();
            //AddNewPlantasWithNewTipoDePlanta();
            AssigTiposDeEnvases();
            System.Console.ReadLine();
        }

        private static void AssigTiposDeEnvases()
        {
            using (var context = new ViveroDbContext())
            {
                var tipoMasetaChica = context.TiposDeEnvases.FirstOrDefault(tp => tp.Descripcion == "Maseta Chica");
                var tipoMasetaMediana = context.TiposDeEnvases.FirstOrDefault(tp => tp.Descripcion == "Maseta Mediana");
                var tipoMasetaGrande = context.TiposDeEnvases.FirstOrDefault(tp => tp.Descripcion == "Maseta Grande");
                //var tipoEnvasePlastico = context.TiposDeEnvases.FirstOrDefault(tp => tp.Descripcion == "Envase Plastico");
                //var tipoEnTierra = context.TiposDeEnvases.FirstOrDefault(tp => tp.Descripcion == "En Tierra");

                foreach (var p in context.Plantas.ToList())
                {
                    if (p.Descripcion.ToLower().Contains("helechos"))
                    {
                        p.TipoDeEnvaseId = tipoMasetaChica.TipoDeEnvaseId;
                    }
                    else if (p.Descripcion.ToLower().Contains("cactus"))
                    {
                        p.TipoDeEnvaseId = tipoMasetaMediana.TipoDeEnvaseId;
                    }
                    else if (p.Descripcion.ToLower().Contains("arbol") || p.Descripcion.ToLower().Contains("palmera"))
                    {
                        p.TipoDeEnvaseId = tipoMasetaGrande.TipoDeEnvaseId;
                    }

                    if (context.Entry(p).State == EntityState.Modified)
                    {
                        context.SaveChanges();

                    }
                }

                System.Console.WriteLine("Cambios ejecutados con exito!");
            }
        }

        private static void AddNewPlantasWithNewTipoDePlanta()
        {
            using (var context = new ViveroDbContext())
            {
                var tipoPlanta = context.TiposDePlantas.FirstOrDefault(tp => tp.Descripcion == "Nuevo tipo de planta");
                if (tipoPlanta == null)
                {
                    return;
                }

                Planta planta1 = new Planta()
                {
                    Descripcion = "Nueva Planta1",
                    Precio = 100,
                    TipoDePlantaId = tipoPlanta.TipoDePlantaId
                };
                Planta planta2 = new Planta()
                {
                    Descripcion = "Nueva Planta2",
                    Precio = 200,
                    TipoDePlantaId = tipoPlanta.TipoDePlantaId
                };

                context.Plantas.AddRange(new List<Planta>() { planta1, planta2 });
                context.SaveChanges();
                System.Console.WriteLine("Plantas agregadas con exito!");
            }
        }

        private static void GetListTiposDePlantas()
        {
            using (var context = new ViveroDbContext())
            {
                var tipoDePlantas = context.TiposDePlantas.ToList();
                foreach (var p in tipoDePlantas)
                {
                    System.Console.WriteLine(p.Descripcion);
                }

                System.Console.WriteLine($"\nCantidad de tipo de plantas : {tipoDePlantas.Count()}");
            }
        }
    }
}
