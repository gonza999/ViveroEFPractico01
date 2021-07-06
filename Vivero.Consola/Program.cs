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
            //UpdatePrecioCostoInPlantas();
            //RemovePlanta();
            //RemovePalmeraInPlantas();
            //RemoveEnvasePlastico();
            //SelectPlantas();
            //SelectTipoPlanta1();
            //SelectPalmeras();

            //SelectPlantasInObjectAnonymus();
            //GroupByPlantas();
            //Select10Plantas();
            //Punto25();
            //Punto26();
            //Punto27();
            //Punto28();
            //Punto29();
            //Punto30();
            Punto31();
            System.Console.ReadLine();
        }

        private static void Punto31()
        {
            using (var context = new ViveroDbContext())
            {
                var cantidadPorPagina = 10;
                var paginas = context.Plantas.Count() / cantidadPorPagina + 1;
                for (int pag = 0; pag < paginas; pag++)
                {
                    var lista = context.Plantas
                        .OrderByDescending(p => p.PlantaId)
                        .Skip(cantidadPorPagina * pag)
                        .Take(cantidadPorPagina)
                        .AsNoTracking()
                        .ToList();
                    foreach (var p in lista)
                    {
                        System.Console.WriteLine($"Planta: {p.Descripcion} {p.PrecioVenta}");
                    }
                }

            }
        }

        private static void Punto30()
        {
            using (var context = new ViveroDbContext())
            {
                var lista = context.Plantas.OrderByDescending(p => p.PrecioVenta)
                    .Skip(10)
                    .Take(5)
                    .ToList();
                foreach (var p in lista)
                {
                    System.Console.WriteLine($"Planta: {p.Descripcion} {p.PrecioVenta}");
                }
            }
        }

        private static void Punto29()
        {
            using (var context = new ViveroDbContext())
            {
                var precioPromedio = context.Plantas.Average(p => p.PrecioVenta);
                var lista = context.Plantas.Where(p => p.PrecioVenta < precioPromedio);
                foreach (var p in lista)
                {
                    System.Console.WriteLine($"Planta: {p.Descripcion} {p.PrecioVenta}");
                }
            }
        }

        private static void Punto28()
        {
            using (var context = new ViveroDbContext())
            {
                var precioMaximo = context.Plantas.Min(p => p.PrecioVenta);
                var planta = context.Plantas.FirstOrDefault(p => p.PrecioVenta == precioMaximo);
                System.Console.WriteLine($"La planta mas barata es {planta.Descripcion}");
            }
        }

        private static void Punto27()
        {
            using (var context = new ViveroDbContext())
            {
                var precioMaximo = context.Plantas.Max(p => p.PrecioVenta);
                var planta = context.Plantas.FirstOrDefault(p => p.PrecioVenta == precioMaximo);
                System.Console.WriteLine($"La planta mas cara es {planta.Descripcion}");
            }
        }

        private static void Punto26()
        {
            using (var context = new ViveroDbContext())
            {
                Random r = new Random();
                var preciofiltro = r.Next(250, 3000);
                if (context.Plantas.Any(p => p.PrecioVenta == preciofiltro))
                {
                    System.Console.WriteLine($"Si hay plantas que tienen un precio igual a {preciofiltro}");
                }
                else
                {
                    System.Console.WriteLine($"No i hay plantas que tienen un precio igual a {preciofiltro}");
                }
            }
        }

        private static void Punto25()
        {
            using (var context = new ViveroDbContext())
            {
                Random r = new Random();
                var preciofiltro = r.Next(250, 3000);
                if (context.Plantas.All(p => p.PrecioVenta < preciofiltro))
                {
                    System.Console.WriteLine($"Todas las plantas tienen un precio inferior a {preciofiltro}");
                }
                else
                {
                    System.Console.WriteLine($"No todas las plantas tienen un precio inferior a {preciofiltro}");
                }
            }
        }

        private static void Select10Plantas()
        {
            using (var context = new ViveroDbContext())
            {
                var preciofiltro = 150;
                var plantas = context.Plantas.Where(p =>
                        p.PrecioVenta < preciofiltro)
                    .Take(10);
                if (!plantas.Any())
                {
                    System.Console.WriteLine("No hay plantas");
                    return;
                }
                foreach (var p in plantas)
                {
                    System.Console.WriteLine($"Planta: {p.Descripcion} {p.PrecioVenta}");
                }

                System.Console.WriteLine("\n\n");
                var plantas2 = context.Plantas.Where(p =>
                        p.PrecioVenta < preciofiltro)
                    .OrderBy(p => p.PrecioVenta).Take(10);
                if (!plantas2.Any())
                {
                    System.Console.WriteLine("No hay plantas");
                    return;
                }
                foreach (var p in plantas2)
                {
                    System.Console.WriteLine($"Planta: {p.Descripcion} {p.PrecioVenta}");
                }
            }
        }

        private static void GroupByPlantas()
        {
            using (var context = new ViveroDbContext())
            {
                var grupo = context.Plantas.GroupBy(p => p.TipoDePlantaId)
                    .ToList();
                foreach (var g in grupo)
                {
                    System.Console.WriteLine($"Clave : {g.Key}");
                    System.Console.WriteLine($"Cantidad de plantas : {g.Count()}");
                    var tipo = context.TiposDePlantas.SingleOrDefault(p => p.TipoDePlantaId == g.Key);
                    System.Console.WriteLine($"Tipo planta : {tipo.Descripcion}\n");
                    foreach (var p in g)
                    {
                        System.Console.WriteLine($"Planta: {p.Descripcion}");
                    }

                    System.Console.ReadLine();
                }
            }
        }

        private static void SelectPlantasInObjectAnonymus()
        {
            using (var context = new ViveroDbContext())
            {
                var plantas = context.Plantas
                    .Include(p => p.TiposDePlanta)
                    .Select(p => new
                    {
                        NombrePlanta = p.Descripcion,
                        TipoPlanta = p.TiposDePlanta.Descripcion
                    })
                    .AsNoTracking()
                    .ToList();
                if (!plantas.Any())
                {
                    System.Console.WriteLine("No hay plantas");
                    return;
                }
                foreach (var p in plantas)
                {
                    System.Console.WriteLine($"Planta: {p.NombrePlanta} {p.TipoPlanta}");
                }

            }
        }

        private static void SelectPalmeras()
        {
            using (var context = new ViveroDbContext())
            {
                var descripcion = "Palmera";
                var plantas = context.Plantas.Where(p =>
                    p.Descripcion.Contains(descripcion))
                    .OrderByDescending(p => p.PrecioVenta).ToList();
                if (!plantas.Any())
                {
                    System.Console.WriteLine("No hay palmeras");
                    return;
                }
                foreach (var p in plantas)
                {
                    System.Console.WriteLine($"Planta: {p.Descripcion} {p.PrecioVenta}");
                }

            }
        }

        private static void SelectTipoPlanta1()
        {
            using (var context = new ViveroDbContext())
            {
                System.Console.WriteLine("Ingresar Tipo de Planta :");
                var tipo = System.Console.ReadLine();
                var plantas = context.Plantas.Include(p => p.TiposDePlanta)
                    .Where(p => p.TiposDePlanta.Descripcion == tipo)
                    .AsNoTracking()
                    .ToList();
                foreach (var p in plantas)
                {
                    System.Console.WriteLine($"Planta: {p.Descripcion} {p.TiposDePlanta.Descripcion}");
                }

            }
        }

        private static void SelectPlantas()
        {
            using (var context = new ViveroDbContext())
            {
                var plantas = context.Plantas.Include(p => p.TiposDePlanta)
                    .AsNoTracking()
                    .ToList();
                foreach (var p in plantas)
                {
                    System.Console.WriteLine($"Planta: {p.Descripcion} {p.TiposDePlanta.Descripcion}");
                }

            }
        }

        private static void RemoveEnvasePlastico()
        {
            using (var context = new ViveroDbContext())
            {
                var descripcion = "Envase Plastico";
                var envase = context.TiposDeEnvases.FirstOrDefault(p =>
                    p.Descripcion.Contains(descripcion));
                if (envase == null)
                {
                    System.Console.WriteLine("Envase inexistente");
                    return;
                }

                System.Console.WriteLine($"Desea borrar el envase {envase.Descripcion}?");
                var respuesta = System.Console.ReadLine();
                if (respuesta.ToUpper() == "S")
                {
                    if (!context.Plantas.Any(p => p.TipoDeEnvaseId == envase.TipoDeEnvaseId))
                    {
                        context.TiposDeEnvases.Remove(envase);
                        context.SaveChanges();
                        System.Console.WriteLine("Envase dada de baja con exito");
                    }
                    else
                    {
                        System.Console.WriteLine("Envase con plantas asociadas");
                    }
                }
                else
                {
                    System.Console.WriteLine("Baja anulada");
                }
            }
        }

        private static void RemovePalmeraInPlantas()
        {
            using (var context = new ViveroDbContext())
            {
                var descripcion = "Palmera";
                var planta = context.Plantas.FirstOrDefault(p =>
                    p.Descripcion.Contains(descripcion));
                if (planta == null)
                {
                    System.Console.WriteLine("Planta inexistente,no hay palmeras");
                    return;
                }

                System.Console.WriteLine($"Desea borrar la planta {planta.Descripcion}?");
                var respuesta = System.Console.ReadLine();
                if (respuesta.ToUpper() == "S")
                {
                    context.Plantas.Remove(planta);
                    context.SaveChanges();
                    System.Console.WriteLine("Planta dada de baja con exito");
                }
            }
        }

        private static void RemovePlanta()
        {
            using (var context = new ViveroDbContext())
            {
                System.Console.WriteLine("Ingrese la planta que va a dar de baja :");
                var plantaId = int.Parse(System.Console.ReadLine());
                var planta = context.Plantas.SingleOrDefault(p => p.PlantaId == plantaId);
                if (planta == null)
                {
                    System.Console.WriteLine("Planta inexistente,clave incorrecta");
                    return;
                }

                System.Console.WriteLine($"Desea borrar la planta {planta.Descripcion}?");
                var respuesta = System.Console.ReadLine();
                if (respuesta.ToUpper() == "S")
                {
                    context.Plantas.Remove(planta);
                    context.SaveChanges();
                    System.Console.WriteLine("Planta dada de baja con exito");
                }

            }
        }

        //private static void UpdatePrecioCostoInPlantas()
        //{
        //    using (var context=new ViveroDbContext())
        //    {
        //        var plantas = context.Plantas.ToList();
        //        foreach (var p in plantas)
        //        {
        //            p.PrecioCosto = p.Precio * 0.5m;
        //        }

        //        context.SaveChanges();
        //        System.Console.WriteLine("Precios costos actualizados");
        //    }
        //}

        private static void DeleteBorrame()
        {
            using (var context = new ViveroDbContext())
            {
                var envaseBorrar = "Borrame";
                var envase = context.TiposDeEnvases.SingleOrDefault(t => t.Descripcion == envaseBorrar);
                if (envase == null)
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
            using (var context = new ViveroDbContext())
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
            using (var context = new ViveroDbContext())
            {
                var envaseBuscado = "Envase Plastico";
                var tipoEnvase =
                    context.TiposDeEnvases.FirstOrDefault(t =>
                        t.Descripcion == envaseBuscado);
                if (tipoEnvase == null)
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
                } while (cantidadCambios < 3);

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

        //private static void AddNewPlantasWithNewTipoDePlanta()
        //{
        //    using (var context = new ViveroDbContext())
        //    {
        //        var tipoPlanta = context.TiposDePlantas.FirstOrDefault(tp => tp.Descripcion == "Nuevo tipo de planta");
        //        if (tipoPlanta == null)
        //        {
        //            return;
        //        }

        //        Planta planta1 = new Planta()
        //        {
        //            Descripcion = "Nueva Planta1",
        //            Precio = 100,
        //            TipoDePlantaId = tipoPlanta.TipoDePlantaId
        //        };
        //        Planta planta2 = new Planta()
        //        {
        //            Descripcion = "Nueva Planta2",
        //            Precio = 200,
        //            TipoDePlantaId = tipoPlanta.TipoDePlantaId
        //        };

        //        context.Plantas.AddRange(new List<Planta>() { planta1, planta2 });
        //        context.SaveChanges();
        //        System.Console.WriteLine("Plantas agregadas con exito!");
        //    }
        //}

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
