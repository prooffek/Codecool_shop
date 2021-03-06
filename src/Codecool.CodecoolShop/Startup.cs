using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
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
            //services.AddDbContext<ShopContext>(option => option.UseSqlServer(Configuration.GetConnectionString("ShopDb")));
            services.AddDbContext<ShopContext>(option => 
                option.UseSqlServer(Configuration.GetConnectionString("ShopDb"))
                );
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
            app.UseSession();
            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });

            //SetupInMemoryDatabases();
            
            /*
            var shopContext = new ShopContext();
            var products = shopContext.Product;
            
            foreach (var product in products)
            {
                Console.Out.WriteLine(product.Name);
            }
            */
        }

        private void SetupInMemoryDatabases()
        {
            IProductDao productDataStore = ProductDaoMemory.GetInstance();
            IProductCategoryDao productCategoryDataStore = ProductCategoryDaoMemory.GetInstance();
            IProductCategoryDao proTemp = new ProductCategoryDaoMemory();
            
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
            
            
            // TravelAgency rainbow = new TravelAgency(){Name = "Rainbow", Description = "Biuro podr????y Rainbow to bogata oferta wakacji samolotem, autokarem wycieczek objazdowych oraz wczas??w za granic??."};
            // travelAgencyDataStore.Add(rainbow);
            // TravelAgency itaka = new TravelAgency(){Name = "Itaka", Description = "Biuro Podr????y ITAKA - organizuje wczasy zagraniczne i wycieczki objazdowe samolotem i autokarem."};
            // travelAgencyDataStore.Add(itaka);
            // TravelAgency tui = new TravelAgency(){Name = "Tui", Description = "Biuro podr????y TUI zorganizuje Tw??j wypoczynek, urlop lub wakacje. Najatrakcyjniejsze oferty last minute na wycieczki, hotele, wczasy za granic??."};
            // travelAgencyDataStore.Add(tui);
            //
            // ProductCategory tourAndLeisure = new ProductCategory {Name = "Objazd i wypoczynek", Description = "Wakacje zwiedzanie i wypoczynek to idealne po????czenie dla os??b, kt??re ceni?? zar??wno aktywny wypoczynek, jak i odrobin?? relaksu i chwil dla siebie." };
            // productCategoryDataStore.Add(tourAndLeisure);
            // ProductCategory leisure = new ProductCategory {Name = "Wypoczynek", Description = "Opis do poprawy." };
            // productCategoryDataStore.Add(leisure);
            // ProductCategory tour = new ProductCategory {Name = "Objazd", Description = "Wakacje zwiedzanie to idealne po????czenie dla os??b, kt??re ceni?? zar??wno aktywny wypoczynek, jak i odrobin?? relaksu i chwil dla siebie." };
            // productCategoryDataStore.Add(tour);
            //
            // Country turkey = new Country {Name = "Turkey", Description = "Turcja zachwyca niezwykle bogat?? histori?? oraz tradycj??, kt??ra podbija serca turyst??w i zapiera dech w piersiach niepowtarzalnymi zabytkami. To w??a??nie tutaj wp??ywy europejskie ????cz?? si?? ze staro??ytnymi i orientalnymi ??ladami."};
            // countryDataStore.Add(turkey);
            // Country egypt = new Country {Name = "Egipt", Description = "Egipt jest prawdziwym rajem dla mi??o??nik??w nurkowania. Kolorowe rafy i kryszta??owo czyste, mieni??ce si?? wszystkimi odcieniami b????kitu Morze Czerwone co roku przyci??gaj?? rzesze pasjonat??w tego sportu. Nie rozczaruj?? si?? te?? tury??ci nastawieni na zwiedzanie - kraj ten bowiem posiada bogactwo ??lad??w przesz??o??ci i zabytki wielu kultur (tajemnicze piramidy, meczety, ??wi??tynie chrze??cija??skie)."};
            // countryDataStore.Add(egypt);
            // Country unitedArabEmirates = new Country {Name = "ZEA", Description = "Zjednoczone Emiraty Arabskie (ZEA) s?? synonimem przepychu i nowoczesno??ci. Jednak kraj ten oferuje znacznie wi??cej ??? tradycyjne bedui??skie wioski, oazy i targi wielb????d??w oraz piaszczyste pla??e. Tutaj znajdziecie wszystko co najwi??ksze i najniezwyklejsze."};
            // countryDataStore.Add(unitedArabEmirates);
            // Country bulgaria = new Country {Name = "Bu??garia", Description = "Bu??garia to jedno z najstarszych pa??stw w Europie, kt??rego walory przez d??ugi czas by??y niedoceniane zar??wno przez mieszka??c??w, jak i turyst??w.Dla turyst??w oferuje zar??wno pobyt w g??rach jak i nad morzem. Odwiedzaj??cych mo??e zach??ci?? bogata w histori?? kultura, a Polak??w dodatkowo blisko???? j??zykowa. Cz??sto odwiedzane wybrze??e Morza Czarnego oraz g??rskie kurorty mog?? sprawi??, ??e zakochamy si?? w tym kraju. Warto r??wnie?? odwiedzi?? stolic?? - Sofi?? i spr??bowa?? znanego na ca??ym ??wiecie bu??garskiego wina."};
            // countryDataStore.Add(bulgaria);
            // Country jordan = new Country {Name = "Jordania", Description = "Terytorium dzisiejszej Jordanii wybra??y niegdy?? na sw?? siedzib?? cywilizacje: nabatejska, kananejska, grecka, rzymska i bizanty??ska. Wszystkie pozostawi??y ??lady na tutejszych ska??ach. Czeka nas zatem zwiedzanie wyj??tkowo ciekawych wykopalisk archeologicznych, m.in. w Petra i Jerach. Z innych atrakcji wymie??my trasy wiod??ce przez s??one pustynie do piaszczystych pla?? w zatoce Akaba, przecinaj?? one okolice Morza Martwego u st??p G??ry Nebo." };
            // countryDataStore.Add(jordan);
            // Country morocco = new Country {Name = "Maroko", Description = "Maroko zachwyca blisko??ci?? oceanu, imponuj??cych pasm g??rskich i pustyni. Jest idealnym rozwi??zaniem na urlop dla tych, kt??rzy uwielbiaj?? s??o??ce i egzotyk??! "};
            // countryDataStore.Add(morocco);
            // Country greece = new Country {Name = "Grecja", Description = "To wyj??tkowe pa??stwo nazywane jest kolebk?? europejskiej cywilizacji. Przyje??d??aj??c tu, trzeba zatrzyma?? si?? przed perfekcyjnie zbudowanym teatrem w Epidauros. Nie wolno nie zanurzy?? si?? w turkusowych wodach oblewaj??cych malutk?? wysepk?? Elafonisi uznawan?? za jedno z najcenniejszych naturalnych miejsc Europy. Tamtejsze pla??e s??yn?? z r????owego piasku."};
            // countryDataStore.Add(greece);
            // Country france = new Country {Name = "Francja", Description = "Francja to najpopularniejszy kraj turystyczny na ??wiecie.Niemal wszyscy tury??ci marz??, by odwiedzi?? Pary?? czy Lazurowe Wybrze??e. Ka??dy zak??tek Francji godny jest uwagi, przyci??ga architektur??, pejza??em lub histori??. Pary?? jest kolebk?? kultury i sztuki. Tu znajduj?? si?? najwi??ksze muzea, teatry, domy mody. Ulice pe??ne s?? artyst??w - muzyk??w i malarzy. Do najbardziej znanych zabytk??w nale??y niew??tpliwie Wie??a Eiffla, kt??ra znajduje si?? na po??udniowym ko??cu P??l Marsowych, nieopodal Sekwany. Ma ona 312 metr??w wysoko??ci i jest orientacyjnym punktem Pary??a. Kolejnym wa??nym zabytkiem jest Wersal, dawna rezydencja kr??lewska, a dzi?? muzeum historii Francji. Mo??na w nim zobaczy?? architektur??, malarstwo, rze??by i wspania??e ogrody. Do najbardziej znanych na ??wiecie ko??cio????w trzeba zaliczy?? ten Sacre - Coeur na wzg??rzu Montmartre. Jego okolice szczeg??lnie upodobali sobie tury??ci, poniewa?? stanowi?? one doskona??y punkt obserwacyjny. B??dac w Pary??u trzeba odwiedzi?? r??wnie?? katedr?? Notre - Dame."};
            // countryDataStore.Add(france);
            //
            //
            // productDataStore.Add(new Product { 
            //     Name = "Turcja Egejska - Su??ta??skie Rarytasy",
            //     DefaultPrice = 1463m, 
            //     Currency = "PLN", 
            //     LengthOfStay = 8,
            //     Description = "Stolica na styku dw??ch kultur - Stambu??, B????kitny Meczet, imponuj??ca Hagia Sophia i Pa??ac Topkapi, fascynuj??ca Kapadocja, legendarna Troja, antyczny Pergamon i Efez, Bawe??niana Twierdza - Pamukkale i termalne ??r??d??a, dla ch??tnych dodatkowy tydzie?? pobytu w okolicy Bodrum, Didim lub Kusadasi.", 
            //     ProductCategory = tourAndLeisure, 
            //     Country = turkey,
            //     City = "",
            //     ImgName = "RTurcjaEgejskaSultanskieRarytasy",
            //     TravelAgency = rainbow });
            //
            // productDataStore.Add(new Product { 
            //     Name = "Turcja - Turcja Licyjska",
            //     DefaultPrice = 1599m, 
            //     Currency = "PLN", 
            //     LengthOfStay = 8,
            //     Description = "Myra - rzymska metropolia, Pamukkale i termalne ??r??d??a, W??w??z Saklikent, kana?? Dalyan, Afrodyzja - ??wi??tynia bogini mi??o??ci, Myra i ??w. Miko??aj, 1 dzie?? wypoczynku w trakcie objazdu", 
            //     ProductCategory = tour, 
            //     Country = turkey,
            //     City = "",
            //     ImgName = "RTurcjaTurcjaLicyjska",
            //     TravelAgency = rainbow });
            //
            // productDataStore.Add(new Product { 
            //     Name = "Egipt - Pot??ga Po??udnia",
            //     DefaultPrice = 1741m, 
            //     Currency = "PLN", 
            //     LengthOfStay = 8,
            //     Description = "Rejs komfortowym statkiem po Nilu ??? Luksor - staro??ytne Teby z imponuj??cym Karnakiem ??? ??wi??tynia Horusa w Edfu i Sobka w Kom Ombo ??? Wielka Tama w Asuanie ??? ??wi??tynia bogini Izydy na wyspie File ??? all inclusive w hotelach w Hurghadzie w cenie ??? za dop??at?? - wypoczynek w s??onecznej Hurghadzie", 
            //     ProductCategory = tourAndLeisure, 
            //     Country = egypt,
            //     City = "",
            //     ImgName = "REgiptPotegaPoludnia",
            //     TravelAgency = rainbow });
            //
            // productDataStore.Add(new Product { 
            //     Name = "Egzotyka Light ??? Emiraty Arabskie - Dubaj",
            //     DefaultPrice = 3292m, 
            //     Currency = "PLN", 
            //     LengthOfStay = 8,
            //     Description = "Dubaj ??? najdro??sze budowle ??wiata ??? Burj Khalifa - najwy??szy drapacz chmur na ??wiecie ??? Dubai Mall - najwi??ksze centrum handlowe ??wiata ??? wypoczynek w wybranym hotelu nad morzem (4 noce)", 
            //     ProductCategory = tourAndLeisure, 
            //     Country = unitedArabEmirates,
            //     City = "Dubaj",
            //     ImgName = "RZEADubaj",
            //     TravelAgency = tui });
            //
            // productDataStore.Add(new Product { 
            //     Name = "Z??ote Wybrze??e Czarnego Morza",
            //     DefaultPrice = 1898m, 
            //     Currency = "PLN", 
            //     LengthOfStay = 8,
            //     Description = "Sozopol - per??a Morza Czarnego ??? Beglik Tash ??? bu??garskie Stonehenge ??? banica i szopska sa??ata ??? gotowanie w wiejskiej chacie ??? pocz??stunek zakrapiany rakij?? ??? Nessebyr ??? najpi??kniejsze bu??garskie miasteczko (UNESCO) ??? rejs statkiem ??? Ba??czik ??? Bia??e Miasto i pa??ac ksi????nej Marii ??? Kaliakra ??? przyl??dek z czerwonych ska?? ??? Warna ??? stolica wybrze??a Morza Czarnego ??? rejs na wysp?? ??w.Anastazji ??? dla ch??tnych wypoczynek w S??onecznym Brzegu (3 lub 7 nocy)", 
            //     ProductCategory = leisure, 
            //     Country = bulgaria,
            //     City = "",
            //     ImgName = "RZloteWybrzezeCzarnegoMorza",
            //     TravelAgency = rainbow });
            //
            // productDataStore.Add(new Product { 
            //     Name = "Dwa morza",
            //     DefaultPrice = 1977m, 
            //     Currency = "PLN", 
            //     LengthOfStay = 8,
            //     Description = "Wypoczynek w Aqabie - kurorcie Kr??lestwa Jordanii (2 dni) ??? Petra - zabytek UNESCO ??? Wypoczynek nad Morzem Martwym (2 dni) ??? Betania - mejsce chrztu Jezusa Chrystusa ??? G??ra Nebo ??? Dla ch??tnych mo??liwo???? przed??u??enia o 7 dni wypoczynku w Aqabie!)", 
            //     ProductCategory = tourAndLeisure, 
            //     Country = jordan,
            //     City = "",
            //     ImgName = "RDwaMorza",
            //     TravelAgency = rainbow });
            //
            // productDataStore.Add(new Product { 
            //     Name = "Maroko - Pustynny Offroad",
            //     DefaultPrice = 3454m, 
            //     Currency = "PLN", 
            //     LengthOfStay = 8,
            //     Description = "Przejazdy samochodami 4x4 ??? Nocleg w obozie nomad??w ??? Zach??d s??o??ca na Saharze ??? Ksar Ait Benhaddou ??? Marrakesz - czerwone miasto ??? dla ch??tnych tydzie?? pobytu w Agadirze", 
            //     ProductCategory = tourAndLeisure, 
            //     Country = morocco,
            //     City = "",
            //     ImgName = "RMarokoPustynnyOffroad",
            //     TravelAgency = itaka });
            //
            // productDataStore.Add(new Product { 
            //     Name = "Wyspy Eptanisa",
            //     DefaultPrice = 2303m, 
            //     Currency = "PLN", 
            //     LengthOfStay = 8,
            //     Description = "Zatoka Wraku na Zakynthos ??? B????kitne Groty ??? Lefkada - najpi??kniejsze pla??e Grecji ??? filmowa Kefalonia ??? jaskinia Drogarati i Melisanni ??? dla ch??tnych Ithaca ??? mo??liwo???? przed??u??enia wypoczynku na Zakynthos", 
            //     ProductCategory = tourAndLeisure, 
            //     Country = greece,
            //     City = "",
            //     ImgName = "RWyspyEptanisa",
            //     TravelAgency = rainbow });
            //
            // productDataStore.Add(new Product { 
            //     Name = "Spiesz si?? powoli - Korfu",
            //     DefaultPrice = 2491m, 
            //     Currency = "PLN", 
            //     LengthOfStay = 8,
            //     Description = "Wyspa Mysia i Liston . Paleokastritsa, BellaVista . P????noc - Kana?? Mi??o??ci - Kassiopi - port i pla??a Imerolia. Dla ch??tnych mo??liwo???? zobaczenia b????kitnych jaski?? i bajecznych zatok w okolicach wysp Paxos i Antipaxos oraz Albanii - mo??liwo???? przed??u??enia pobytu na Korfu", 
            //     ProductCategory = tourAndLeisure, 
            //     Country = greece,
            //     City = "",
            //     ImgName = "RSpieszSiePowoliKorfu",
            //     TravelAgency = rainbow });
            //
            // productDataStore.Add(new Product { 
            //     Name = "Pary?? Light",
            //     DefaultPrice = 1149m, 
            //     Currency = "PLN", 
            //     LengthOfStay = 6,
            //     Description = "Wie??a Eiffla - dla ch??tnych wjazd na szczyt wie??y ??? Montmartre - dzielnica artyst??w ??? Pola Elizejskie - g????wna ulica ??wiata ??? Dzielnica ??aci??ska i Ogrody Luksemburskie ??? Muzeum Impresjonist??w ??? spokojny i relaksuj??cy program", 
            //     ProductCategory = tour, 
            //     Country = france,
            //     City = "",
            //     ImgName = "RParyzLight",
            //     TravelAgency = itaka });
        }
    }
}
