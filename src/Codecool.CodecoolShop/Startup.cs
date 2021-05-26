using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codecool.CodecoolShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Product/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });

            SetupInMemoryDatabases();
        }

        private void SetupInMemoryDatabases()
        {
            IProductDao productDataStore = ProductDaoMemory.GetInstance();
            IProductCategoryDao productCategoryDataStore = ProductCategoryDaoMemory.GetInstance();
            ITravelAgencyDao travelAgencyDataStore = TravelAgencyDaoMemory.GetInstance();

            TravelAgency rainbow = new TravelAgency(){Id = 0, Name = "Rainbow", Description = "Biuro podróży Rainbow to bogata oferta wakacji samolotem, autokarem wycieczek objazdowych oraz wczasów za granicą."};
            travelAgencyDataStore.Add(rainbow);
            TravelAgency itaka = new TravelAgency(){Id = 1, Name = "Itaka", Description = "Biuro Podróży ITAKA - organizuje wczasy zagraniczne i wycieczki objazdowe samolotem i autokarem."};
            travelAgencyDataStore.Add(itaka);
            TravelAgency tui = new TravelAgency(){Id = 2, Name = "Tui", Description = "Biuro podróży TUI zorganizuje Twój wypoczynek, urlop lub wakacje. Najatrakcyjniejsze oferty last minute na wycieczki, hotele, wczasy za granicą."};
            travelAgencyDataStore.Add(itaka);
            
            ProductCategory tourAndLeisure = new ProductCategory {Id = 0, Name = "Objazd i wypoczynek", Description = "Wakacje zwiedzanie i wypoczynek to idealne połączenie dla osób, które cenią zarówno aktywny wypoczynek, jak i odrobinę relaksu i chwil dla siebie." };
            productCategoryDataStore.Add(tourAndLeisure);
            
            ProductCategory tour = new ProductCategory {Id = 1, Name = "Objazd", Description = "Wakacje zwiedzanie to idealne połączenie dla osób, które cenią zarówno aktywny wypoczynek, jak i odrobinę relaksu i chwil dla siebie." };
            productCategoryDataStore.Add(tourAndLeisure);
            
            
            productDataStore.Add(new Product { 
                Name = "Turcja Egejska - Sułtańskie Rarytasy",
                DefaultPrice = 1463m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Stolica na styku dwóch kultur - Stambuł, Błękitny Meczet, imponująca Hagia Sophia i Pałac Topkapi, fascynująca Kapadocja, legendarna Troja, antyczny Pergamon i Efez, Bawełniana Twierdza - Pamukkale i termalne źródła, dla chętnych dodatkowy tydzień pobytu w okolicy Bodrum, Didim lub Kusadasi.", 
                ProductCategory = tourAndLeisure, 
                Country = "Turcja",
                City = "",
                ImgName = "RTurcjaEgejskaSultanskieRarytasy",
                TravelAgency = rainbow });

            productDataStore.Add(new Product { 
                Name = "Turcja - Turcja Licyjska",
                DefaultPrice = 1599m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Myra - rzymska metropolia, Pamukkale i termalne źródła, Wąwóz Saklikent, kanał Dalyan, Afrodyzja - świątynia bogini miłości, Myra i św. Mikołaj, 1 dzień wypoczynku w trakcie objazdu", 
                ProductCategory = tourAndLeisure, 
                Country = "Turcja",
                City = "",
                ImgName = "RTurcjaTurcjaLicyjska",
                TravelAgency = rainbow });

            productDataStore.Add(new Product { 
                Name = "Egipt - Potęga Południa",
                DefaultPrice = 1741m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Rejs komfortowym statkiem po Nilu • Luksor - starożytne Teby z imponującym Karnakiem • świątynia Horusa w Edfu i Sobka w Kom Ombo • Wielka Tama w Asuanie • świątynia bogini Izydy na wyspie File • all inclusive w hotelach w Hurghadzie w cenie • za dopłatą - wypoczynek w słonecznej Hurghadzie", 
                ProductCategory = tourAndLeisure, 
                Country = "Egipt",
                City = "",
                ImgName = "REgiptPotegaPoludnia",
                TravelAgency = rainbow });

            productDataStore.Add(new Product { 
                Name = "Egzotyka Light – Emiraty Arabskie - Dubaj",
                DefaultPrice = 3292m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Dubaj – najdroższe budowle świata • Burj Khalifa - najwyższy drapacz chmur na świecie • Dubai Mall - największe centrum handlowe świata • wypoczynek w wybranym hotelu nad morzem (4 noce)", 
                ProductCategory = tourAndLeisure, 
                Country = "Zjednoczone Emiraty Arabskie",
                City = "Dubaj",
                ImgName = "RZEADubaj",
                TravelAgency = tui });

            productDataStore.Add(new Product { 
                Name = "Złote Wybrzeże Czarnego Morza",
                DefaultPrice = 1898m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Sozopol - perła Morza Czarnego • Beglik Tash – bułgarskie Stonehenge • banica i szopska sałata – gotowanie w wiejskiej chacie – poczęstunek zakrapiany rakiją • Nessebyr – najpiękniejsze bułgarskie miasteczko (UNESCO) – rejs statkiem • Bałczik – Białe Miasto i pałac księżnej Marii • Kaliakra – przylądek z czerwonych skał • Warna – stolica wybrzeża Morza Czarnego • rejs na wyspę św.Anastazji • dla chętnych wypoczynek w Słonecznym Brzegu (3 lub 7 nocy)", 
                ProductCategory = tourAndLeisure, 
                Country = "Bługaria",
                City = "",
                ImgName = "RZloteWybrzezeCzarnegoMorza",
                TravelAgency = rainbow });

            productDataStore.Add(new Product { 
                Name = "Dwa morza",
                DefaultPrice = 1977m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Wypoczynek w Aqabie - kurorcie Królestwa Jordanii (2 dni) • Petra - zabytek UNESCO • Wypoczynek nad Morzem Martwym (2 dni) • Betania - mejsce chrztu Jezusa Chrystusa • Góra Nebo • Dla chętnych możliwość przedłużenia o 7 dni wypoczynku w Aqabie!)", 
                ProductCategory = tour, 
                Country = "Jordania",
                City = "",
                ImgName = "RDwaMorza",
                TravelAgency = rainbow });

            productDataStore.Add(new Product { 
                Name = "Maroko - Pustynny Offroad",
                DefaultPrice = 3454m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Przejazdy samochodami 4x4 • Nocleg w obozie nomadów • Zachód słońca na Saharze • Ksar Ait Benhaddou • Marrakesz - czerwone miasto • dla chętnych tydzień pobytu w Agadirze", 
                ProductCategory = tour, 
                Country = "Maroko",
                City = "",
                ImgName = "RMarokoPustynnyOffroad",
                TravelAgency = itaka });

            productDataStore.Add(new Product { 
                Name = "Wyspy Eptanisa",
                DefaultPrice = 2303m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Zatoka Wraku na Zakynthos • Błękitne Groty • Lefkada - najpiękniejsze plaże Grecji • filmowa Kefalonia • jaskinia Drogarati i Melisanni • dla chętnych Ithaca • możliwość przedłużenia wypoczynku na Zakynthos", 
                ProductCategory = tour, 
                Country = "Grecja",
                City = "",
                ImgName = "RWyspyEptanisa",
                TravelAgency = rainbow });

            productDataStore.Add(new Product { 
                Name = "Spiesz się powoli - Korfu",
                DefaultPrice = 2491m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Wyspa Mysia i Liston . Paleokastritsa, BellaVista . Północ - Kanał Miłości - Kassiopi - port i plaża Imerolia. Dla chętnych możliwość zobaczenia błękitnych jaskiń i bajecznych zatok w okolicach wysp Paxos i Antipaxos oraz Albanii - możliwość przedłużenia pobytu na Korfu", 
                ProductCategory = tour, 
                Country = "Grecja",
                City = "",
                ImgName = "RSpieszSiePowoliKorfu",
                TravelAgency = rainbow });

            productDataStore.Add(new Product { 
                Name = "Paryż Light",
                DefaultPrice = 1149m, 
                Currency = "PLN", 
                LengthOfStay = 6,
                Description = "Wieża Eiffla - dla chętnych wjazd na szczyt wieży • Montmartre - dzielnica artystów • Pola Elizejskie - główna ulica świata • Dzielnica Łacińska i Ogrody Luksemburskie • Muzeum Impresjonistów • spokojny i relaksujący program", 
                ProductCategory = tour, 
                Country = "Francja",
                City = "",
                ImgName = "RParyzLight",
                TravelAgency = rainbow });
            
        }
    }
}
