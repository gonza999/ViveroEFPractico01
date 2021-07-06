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
            //GetListTiposDePlantas();
            //AddNewPlantasWithNewTipoDePlanta();
            //AssigTiposDeEnvases();
            //AddTipoEnvaseIn3Plantas();
            //AddBorrameTipoDeEnvase();
            //DeleteBorrame();
            UpdatePrecioCostoInPlantas();
            System.Console.ReadLine();
        }

        private static void UpdatePrecioCostoInPlantas()
        {
            using (var context=new ViveroDbContext())
            {
                var plantas = context.Plantas.ToList();
                foreach (var p in plantas)
                {
                    p.PrecioCosto = p.Precio * 0.5m;
                }

                context.SaveChanges();
                System.Console.WriteLine("Precios costos actualizados");
            }
        }

        private static void DeleteBorrame()
        {
            using (var context=new ViveroDbContext())
            {
                var envaseBorrar = "Borrame";
                var envase = context.TiposDeEnvases.SingleOrDefault(t => t.Descripcion == envaseBorrar);
                if (envase==null)
                {
                    System.Console.WriteLine("Tipo de envase inexistente");
                    return;
                    
                }

                context.TiposDeEnvases.Remove(envase);
                context.SaveChanges();
                System.Console.WriteLine("Envase borrado con exito");
            }
        }

        private static void AddBorrameTipoDeEnvase()
        {
            using (var context=new ViveroDbContext())
            {
                TipoDeEnvase tipoDeEnvase = new TipoDeEnvase()
                {
                    Descripcion = "Borrame"
                };
                context.TiposDeEnvases.Add(tipoDeEnvase);
                context.SaveChanges();
                System.Console.WriteLine("Tipo de envase agregado con exito");


                Random r = new Random();
                var cantidadCambios = 0;
                do
                {
                    var plantaid = r.Next(167, 328);
                    var planta = context.Plantas.FirstOrDefault(p => p.PlantaId == plantaid);
                    if (planta != null)
                    {
                        planta.TipoDeEnvaseId = tipoDeEnvase.TipoDeEnvaseId;
                        cantidadCambios++;
                        System.Console.WriteLine($"{planta.Descripcion}");
                        System.Console.WriteLine($"Cambio procesado. Cantidad de cambios procesados : {cantidadCambios}");
                    }
                } while (cantidadCambios < 2);

                context.SaveChanges();
            }
        }

        private static void AddTipoEnvaseIn3Plantas()
        {
            using (var context=new ViveroDbContext())
            {
                var envaseBuscado = "Envase Plastico";
                var tipoEnvase = 
                    context.TiposDeEnvases.FirstOrDefault(t =>
                        t.Descripcion == envaseBuscado);
                if (tipoEnvase==null)
                {
                    System.Console.WriteLine("Tipo de envase inexistente");
                    return;
                }

                Random r = new Random();
                var cantidadCambios = 0;
                do
                {
                    var plantaid = r.Next(167, 328);
                    var planta = context.Plantas.FirstOrDefault(p => p.PlantaId == plantaid);
                    if (planta != null)
                    {
                        planta.TipoDeEnvaseId = tipoEnvase.TipoDeEnvaseId;
                        cantidadCambios++;
                        System.Console.WriteLine($"Cambio procesado. Cantidad de cambios procesados : {cantidadCambios}");
                    } 
                } while (cantidadCambios<3); 
                
                context.SaveChanges();
                System.Console.WriteLine("Cambios realizados");
            }
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
