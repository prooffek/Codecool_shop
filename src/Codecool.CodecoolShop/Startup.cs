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
            services.AddSession();
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
            app.UseSession();

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
            
            
            /*
            ISupplierDao supplierDataStore = SupplierDaoMemory.GetInstance();

            var amazon = new TravelAgency(){Name = "Amazon", Description = "Digital content and services"};
            supplierDataStore.Add(amazon);
            var lenovo = new TravelAgency(){Name = "Lenovo", Description = "Computers"};
            supplierDataStore.Add(lenovo);
            ProductCategory tablet = new ProductCategory {Name = "Tablet", Department = "Hardware", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
            productCategoryDataStore.Add(tablet);
            productDataStore.Add(new Product { Name = "Amazon Fire", DefaultPrice = 49.9m, Currency = "USD", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = tablet, TravelAgency = amazon });
            productDataStore.Add(new Product { Name = "Lenovo IdeaPad Miix 700", DefaultPrice = 479.0m, Currency = "USD", Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", ProductCategory = tablet, TravelAgency = lenovo });
            productDataStore.Add(new Product { Name = "Amazon Fire HD 8", DefaultPrice = 89.0m, Currency = "USD", Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.", ProductCategory = tablet, TravelAgency = amazon });
            */

            ITravelAgencyDao travelAgencyDataStore = TravelAgencyDaoMemory.GetInstance();
            ICountryDao countryDataStore = CountryDaoMemory.GetInstance();
            
            
            TravelAgency rainbow = new TravelAgency(){Name = "Rainbow", Description = "Biuro podróży Rainbow to bogata oferta wakacji samolotem, autokarem wycieczek objazdowych oraz wczasów za granicą."};
            travelAgencyDataStore.Add(rainbow);
            TravelAgency itaka = new TravelAgency(){Name = "Itaka", Description = "Biuro Podróży ITAKA - organizuje wczasy zagraniczne i wycieczki objazdowe samolotem i autokarem."};
            travelAgencyDataStore.Add(itaka);
            TravelAgency tui = new TravelAgency(){Name = "Tui", Description = "Biuro podróży TUI zorganizuje Twój wypoczynek, urlop lub wakacje. Najatrakcyjniejsze oferty last minute na wycieczki, hotele, wczasy za granicą."};
            travelAgencyDataStore.Add(tui);
            
            ProductCategory tourAndLeisure = new ProductCategory {Name = "Objazd i wypoczynek", Description = "Wakacje zwiedzanie i wypoczynek to idealne połączenie dla osób, które cenią zarówno aktywny wypoczynek, jak i odrobinę relaksu i chwil dla siebie." };
            productCategoryDataStore.Add(tourAndLeisure);
            ProductCategory leisure = new ProductCategory {Name = "Wypoczynek", Description = "Opis do poprawy." };
            productCategoryDataStore.Add(leisure);
            ProductCategory tour = new ProductCategory {Name = "Objazd", Description = "Wakacje zwiedzanie to idealne połączenie dla osób, które cenią zarówno aktywny wypoczynek, jak i odrobinę relaksu i chwil dla siebie." };
            productCategoryDataStore.Add(tour);

            Country turkey = new Country {Name = "Turkey", Description = "Turcja zachwyca niezwykle bogatą historią oraz tradycją, która podbija serca turystów i zapiera dech w piersiach niepowtarzalnymi zabytkami. To właśnie tutaj wpływy europejskie łączą się ze starożytnymi i orientalnymi śladami."};
            countryDataStore.Add(turkey);
            Country egypt = new Country {Name = "Egipt", Description = "Egipt jest prawdziwym rajem dla miłośników nurkowania. Kolorowe rafy i kryształowo czyste, mieniące się wszystkimi odcieniami błękitu Morze Czerwone co roku przyciągają rzesze pasjonatów tego sportu. Nie rozczarują się też turyści nastawieni na zwiedzanie - kraj ten bowiem posiada bogactwo śladów przeszłości i zabytki wielu kultur (tajemnicze piramidy, meczety, świątynie chrześcijańskie)."};
            countryDataStore.Add(egypt);
            Country unitedArabEmirates = new Country {Name = "Zjednoczone Emiraty Arabskie", Description = "Zjednoczone Emiraty Arabskie (ZEA) są synonimem przepychu i nowoczesności. Jednak kraj ten oferuje znacznie więcej – tradycyjne beduińskie wioski, oazy i targi wielbłądów oraz piaszczyste plaże. Tutaj znajdziecie wszystko co największe i najniezwyklejsze."};
            countryDataStore.Add(unitedArabEmirates);
            Country bulgaria = new Country {Name = "Bułgaria", Description = "Bułgaria to jedno z najstarszych państw w Europie, którego walory przez długi czas były niedoceniane zarówno przez mieszkańców, jak i turystów.Dla turystów oferuje zarówno pobyt w górach jak i nad morzem. Odwiedzających może zachęcić bogata w historię kultura, a Polaków dodatkowo bliskość językowa. Często odwiedzane wybrzeże Morza Czarnego oraz górskie kurorty mogą sprawić, że zakochamy się w tym kraju. Warto również odwiedzić stolicę - Sofię i spróbować znanego na całym świecie bułgarskiego wina."};
            countryDataStore.Add(bulgaria);
            Country jordan = new Country {Name = "Jordania", Description = "Terytorium dzisiejszej Jordanii wybrały niegdyś na swą siedzibę cywilizacje: nabatejska, kananejska, grecka, rzymska i bizantyńska. Wszystkie pozostawiły ślady na tutejszych skałach. Czeka nas zatem zwiedzanie wyjątkowo ciekawych wykopalisk archeologicznych, m.in. w Petra i Jerach. Z innych atrakcji wymieńmy trasy wiodące przez słone pustynie do piaszczystych plaż w zatoce Akaba, przecinają one okolice Morza Martwego u stóp Góry Nebo." };
            countryDataStore.Add(jordan);
            Country morocco = new Country {Name = "Maroko", Description = "Maroko zachwyca bliskością oceanu, imponujących pasm górskich i pustyni. Jest idealnym rozwiązaniem na urlop dla tych, którzy uwielbiają słońce i egzotykę! "};
            countryDataStore.Add(morocco);
            Country greece = new Country {Name = "Grecja", Description = "To wyjątkowe państwo nazywane jest kolebką europejskiej cywilizacji. Przyjeżdżając tu, trzeba zatrzymać się przed perfekcyjnie zbudowanym teatrem w Epidauros. Nie wolno nie zanurzyć się w turkusowych wodach oblewających malutką wysepkę Elafonisi uznawaną za jedno z najcenniejszych naturalnych miejsc Europy. Tamtejsze plaże słyną z różowego piasku."};
            countryDataStore.Add(greece);
            Country france = new Country {Name = "Francja", Description = "Francja to najpopularniejszy kraj turystyczny na świecie.Niemal wszyscy turyści marzą, by odwiedzić Paryż czy Lazurowe Wybrzeże. Każdy zakątek Francji godny jest uwagi, przyciąga architekturą, pejzażem lub historią. Paryż jest kolebką kultury i sztuki. Tu znajdują się największe muzea, teatry, domy mody. Ulice pełne są artystów - muzyków i malarzy. Do najbardziej znanych zabytków należy niewątpliwie Wieża Eiffla, która znajduje się na południowym końcu Pól Marsowych, nieopodal Sekwany. Ma ona 312 metrów wysokości i jest orientacyjnym punktem Paryża. Kolejnym ważnym zabytkiem jest Wersal, dawna rezydencja królewska, a dziś muzeum historii Francji. Można w nim zobaczyć architekturę, malarstwo, rzeźby i wspaniałe ogrody. Do najbardziej znanych na świecie kościołów trzeba zaliczyć ten Sacre - Coeur na wzgórzu Montmartre. Jego okolice szczególnie upodobali sobie turyści, ponieważ stanowią one doskonały punkt obserwacyjny. Będac w Paryżu trzeba odwiedzić również katedrę Notre - Dame."};
            countryDataStore.Add(france);
            
            
            productDataStore.Add(new Product { 
                Name = "Turcja Egejska - Sułtańskie Rarytasy",
                DefaultPrice = 1463m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Stolica na styku dwóch kultur - Stambuł, Błękitny Meczet, imponująca Hagia Sophia i Pałac Topkapi, fascynująca Kapadocja, legendarna Troja, antyczny Pergamon i Efez, Bawełniana Twierdza - Pamukkale i termalne źródła, dla chętnych dodatkowy tydzień pobytu w okolicy Bodrum, Didim lub Kusadasi.", 
                ProductCategory = tourAndLeisure, 
                Country = turkey,
                City = "",
                ImgName = "RTurcjaEgejskaSultanskieRarytasy",
                TravelAgency = rainbow });

            productDataStore.Add(new Product { 
                Name = "Turcja - Turcja Licyjska",
                DefaultPrice = 1599m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Myra - rzymska metropolia, Pamukkale i termalne źródła, Wąwóz Saklikent, kanał Dalyan, Afrodyzja - świątynia bogini miłości, Myra i św. Mikołaj, 1 dzień wypoczynku w trakcie objazdu", 
                ProductCategory = tour, 
                Country = turkey,
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
                Country = egypt,
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
                Country = unitedArabEmirates,
                City = "Dubaj",
                ImgName = "RZEADubaj",
                TravelAgency = tui });

            productDataStore.Add(new Product { 
                Name = "Złote Wybrzeże Czarnego Morza",
                DefaultPrice = 1898m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Sozopol - perła Morza Czarnego • Beglik Tash – bułgarskie Stonehenge • banica i szopska sałata – gotowanie w wiejskiej chacie – poczęstunek zakrapiany rakiją • Nessebyr – najpiękniejsze bułgarskie miasteczko (UNESCO) – rejs statkiem • Bałczik – Białe Miasto i pałac księżnej Marii • Kaliakra – przylądek z czerwonych skał • Warna – stolica wybrzeża Morza Czarnego • rejs na wyspę św.Anastazji • dla chętnych wypoczynek w Słonecznym Brzegu (3 lub 7 nocy)", 
                ProductCategory = leisure, 
                Country = bulgaria,
                City = "",
                ImgName = "RZloteWybrzezeCzarnegoMorza",
                TravelAgency = rainbow });

            productDataStore.Add(new Product { 
                Name = "Dwa morza",
                DefaultPrice = 1977m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Wypoczynek w Aqabie - kurorcie Królestwa Jordanii (2 dni) • Petra - zabytek UNESCO • Wypoczynek nad Morzem Martwym (2 dni) • Betania - mejsce chrztu Jezusa Chrystusa • Góra Nebo • Dla chętnych możliwość przedłużenia o 7 dni wypoczynku w Aqabie!)", 
                ProductCategory = tourAndLeisure, 
                Country = jordan,
                City = "",
                ImgName = "RDwaMorza",
                TravelAgency = rainbow });

            productDataStore.Add(new Product { 
                Name = "Maroko - Pustynny Offroad",
                DefaultPrice = 3454m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Przejazdy samochodami 4x4 • Nocleg w obozie nomadów • Zachód słońca na Saharze • Ksar Ait Benhaddou • Marrakesz - czerwone miasto • dla chętnych tydzień pobytu w Agadirze", 
                ProductCategory = tourAndLeisure, 
                Country = morocco,
                City = "",
                ImgName = "RMarokoPustynnyOffroad",
                TravelAgency = itaka });

            productDataStore.Add(new Product { 
                Name = "Wyspy Eptanisa",
                DefaultPrice = 2303m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Zatoka Wraku na Zakynthos • Błękitne Groty • Lefkada - najpiękniejsze plaże Grecji • filmowa Kefalonia • jaskinia Drogarati i Melisanni • dla chętnych Ithaca • możliwość przedłużenia wypoczynku na Zakynthos", 
                ProductCategory = tourAndLeisure, 
                Country = greece,
                City = "",
                ImgName = "RWyspyEptanisa",
                TravelAgency = rainbow });

            productDataStore.Add(new Product { 
                Name = "Spiesz się powoli - Korfu",
                DefaultPrice = 2491m, 
                Currency = "PLN", 
                LengthOfStay = 8,
                Description = "Wyspa Mysia i Liston . Paleokastritsa, BellaVista . Północ - Kanał Miłości - Kassiopi - port i plaża Imerolia. Dla chętnych możliwość zobaczenia błękitnych jaskiń i bajecznych zatok w okolicach wysp Paxos i Antipaxos oraz Albanii - możliwość przedłużenia pobytu na Korfu", 
                ProductCategory = tourAndLeisure, 
                Country = greece,
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
                Country = france,
                City = "",
                ImgName = "RParyzLight",
                TravelAgency = itaka });
        }
    }
}
