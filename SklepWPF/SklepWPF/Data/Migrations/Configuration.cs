namespace SklepWPF.Migrations
{
	using SklepWPF.Data;
	using SklepWPF.Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SklepWPF.Data.MyDbContext>
    {
		private readonly MyDbContext _db= MyDbContext.Create();
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
			this.MigrationsDirectory = @"Data\Migrations";
		}

        protected override void Seed(SklepWPF.Data.MyDbContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.
			var _destinationPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
			_destinationPath = _destinationPath.Replace("\\", "/");
			_destinationPath = _destinationPath.Replace("/bin/Debug", "");

			var houseCategory = new Category { Id = 1, Name = "Dom i ogród" };
			var childCategory = new Category { Id = 2, Name = "Dziecko" };
			var electronicCategory = new Category { Id = 3, Name = "Elektronika" };
			var beautyCategory = new Category { Id = 4, Name = "Uroda" };
			var fashionCategory = new Category { Id = 5, Name = "Moda" };
			var carCategory = new Category { Id = 6, Name = "Samochody" };
			var sportCategory = new Category { Id = 7, Name = "Sport" };
			var healthCategory = new Category { Id = 8, Name = "Zdrowie" };
			var companyCategory = new Category { Id = 9, Name = "Firma" };



			_db.Categories.AddOrUpdate(houseCategory);
			_db.Categories.AddOrUpdate(childCategory);
			_db.Categories.AddOrUpdate(electronicCategory);
			_db.Categories.AddOrUpdate(beautyCategory);
			_db.Categories.AddOrUpdate(fashionCategory);
			_db.Categories.AddOrUpdate(carCategory);
			_db.Categories.AddOrUpdate(sportCategory);
			_db.Categories.AddOrUpdate(healthCategory);
			_db.Categories.AddOrUpdate(companyCategory);

			User user1 = new User
            {
                Id = 1,
                Name = "Kamil",
                Surname = "Kapliński",
                Nickname = "Kamil",
                PhoneNumber = "781787771",
                City = "Białystok",
                Email = "123",
                Password = "123",
                PostalCode = "111",
                PaymentMethod = Enums.PaymentMethod.ByCash,
                StreetName = "123",
                IsAdmin = true
            };

            User user2 = new User
            {
                Id = 2,
                Name = "Mati",
                Surname = "Kapliński",
                Nickname = "Mati",
                PhoneNumber = "781787771",
                City = "Białystok",
                Email = "123",
                Password = "123",
                PostalCode = "111",
                PaymentMethod = Enums.PaymentMethod.ByCash,
                StreetName = "123",
                IsAdmin = true,
            };

            User user3 = new User
            {
                Id = 3,
                Name = "Łukasz",
                Surname = "Kapliński",
                Nickname = "Łukasz",
                PhoneNumber = "781787771",
                City = "Białystok",
                Email = "123",
                Password = "123",
                PostalCode = "111",
                PaymentMethod = Enums.PaymentMethod.ByCash,
                StreetName = "123"
            };

            _db.Users.AddOrUpdate(user1);
            _db.Users.AddOrUpdate(user2);
            _db.Users.AddOrUpdate(user3);

            _db.Messages.AddOrUpdate(new Message
            {
                Id = 1,
                AuthorId = user3.Id,
                Senders = new List<User> { user3 },
                Receivers = new List<User> { user1, user2 },
                Title = "Wiadomość testowa",
                Content = "lalalal \n\nŁukasz Hryniewicki " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\n\n",
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                AuthorFullName = "Łukasz Hryniewicki",
                ClientSeen = true
            });

            _db.Messages.AddOrUpdate(new Message
            {
                Id = 2,
                AuthorId = user3.Id,
                Senders = new List<User> { user3 },
                Receivers = new List<User> { user1, user2 },
                Title = "Wiadomość testowa1",
                Content = "lalalaxxxxxxl \n\nŁukasz Hryniewicki " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\n\n",
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                AuthorFullName = "Łukasz Hryniewicki",
                ClientSeen = true
            });

            _db.Messages.AddOrUpdate(new Message
            {
                Id = 3,
                AuthorId = user3.Id,
                Senders = new List<User> { user3 },
                Receivers = new List<User> { user1, user2 },
                Title = "Wiadomość testowa2",
                Content = "lalasadfsdasdasdlal \n\nŁukasz Hryniewicki " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\n\n",
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                AuthorFullName = "Łukasz Hryniewicki",
                ClientSeen = true
            });

            _db.Messages.AddOrUpdate(new Message
            {
                Id = 4,
                AuthorId = user3.Id,
                Senders = new List<User> { user3 },
                Receivers = new List<User> { user1, user2 },
                Title = "Wiadomość testowa3",
                Content = "lalalasdsadasddwwal \n\nŁukasz Hryniewicki " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\n\n",
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                AuthorFullName = "Łukasz Hryniewicki",
                ClientSeen = true
            });

            _db.Messages.AddOrUpdate(new Message
            {
                Id = 5,
                AuthorId = user3.Id,
                Senders = new List<User> { user3 },
                Receivers = new List<User> { user1, user2 },
                Title = "Wiadomość testowa4",
                Content = "lalalawdwdqdqwdqwal \n\nŁukasz Hryniewicki " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\n\n",
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                AuthorFullName = "Łukasz Hryniewicki",
                ClientSeen = true
            });

            _db.Messages.AddOrUpdate(new Message
            {
                Id = 6,
                AuthorId = user3.Id,
                Senders = new List<User> { user3 },
                Receivers = new List<User> { user1, user2 },
                Title = "Wiadomość testowa5",
                Content = "lalalaasdasdasasdasdasdasdl \n\nŁukasz Hryniewicki " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\n\n",
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                AuthorFullName = "Łukasz Hryniewicki",
                ClientSeen = true
            });

			_db.Products.AddOrUpdate(new Product
			{
				Id = 1,
				Brand = "Kruger&Matz",
				Description = "Cyfrowa jakość sygnału- Wiedząc jak ważna jest uniwersalność i komfort użytkowania," +
				"32 - calowy telewizor Kruger & Matz został wyposażony w tuner DVB - T2," +
				"który pozwoli na odbiór kanałów cyfrowej telewizji naziemnej.Teraz nie musisz już martwić " +
				"się o zakup dodatkowych urządzeń! " + "Zwiększ możliwości Cyfrowa telewizja to za mało ? Porty," +
				"w jakie został wyposażony telewizor Kruger & Matz,znacznie rozbudowują możliwości tego urządzenia." +
				"Posiada ono między innymi dwa popularne i wydajne złącza HDMI," +
				"które zagwarantują wysoką jakość przesyłanego obrazu.",
				Name = "Telewizor 32'' Kruger&Matz, HD, 2 HDMI, DVB-T2",
				Price = 519.00,
				ImagePath = _destinationPath + "KrugerTv.png",
				Quantity = 53,
				Categories = new List<Category> { electronicCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 2,
				Brand = "TCL",
				Description = "Jakość obrazu HDR: niezrównana czerń, wyższy kontrast i żywe kolory-Najnowszym standardem dla " +
				"treści UHD jest High Dynamic Range.HDR daje efekt lśnienia jasności,niezwykle precyzyjnego koloru i olśniewających " +
				"szczegółów obrazu z dokładną reprodukcją jasnych i ciemnych odcieni.Pozwól," +
				"abyś doświadczał treści HDR ze wszystkich źródeł.Dolby Audio zapewnia bogaty,czysty i mocny dźwięk w telewizorze " +
				"Wykorzystaj w pełni swój potencjał dzięki dynamicznemu, wysokiej jakości dźwiękowi przestrzennemu dzięki " +
				"Dolby Audio.Niezależnie od tego,czy oglądasz filmy,programy telewizyjne,filmy online czy koncerty," +
				"Dolby Audio gwarantuje jakość dźwięku,która spełnia Twoje oczekiwania.SMART TV 3.0 dla łatwiejszego dostępu do" +
				"zawartości 4K UHD HDR. Dzięki SMART TV 3.0 korzystanie z ulubionych treści jest teraz łatwiejsze niż " +
				"kiedykolwiek dzięki najnowszym i często używanym kategoriom.Na szczycie, dzięki aplikacji TV +App Store, możesz pobierać ulubione aplikacje," +
				" filmy w wysokiej rozdzielczości, filmy i gry.Netflix i Youtube mają jakość 4K HDR na SMART TV 3.0",
				Name = "Telewizor TCL 49DP600 49'' LED 4K SmartTV +HDMI",
				Price = 1359.15,
				ImagePath = _destinationPath + "TclTV.png",
				Quantity = 121,
				Categories = new List<Category> { electronicCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 3,
				Brand = "Samsung",
				Description = "Przekątna ekranu: 43,0. Technologia obrazu: Telewizory 4K. Rozdzielczość: 3840 x 2160. " +
				"Tuner TV: DVB - C - cyfrowy kablowy, Analogowy, DVB - T2 - cyfrowy naziemny, DVB - S2 - cyfrowy satelitarny. " +
				"Interfejsy: USB 3.0, RJ - 45(Ethernet LAN),Common Interface(CI)",
				Name= "Telewizor Samsung UE43RU7402 43'' LED 4K SmartTV",
				Price = 2279.99,
				ImagePath = _destinationPath + "SamsungTv.png",
				Quantity = 89,
				Categories = new List<Category> { electronicCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 4,
				Brand = "Medion",
				Name= "TELEWIZOR LED 40'' FULL HD SMART TV MPEG-4 WI-FI",
				Description = "Przedmiotem naszej oferty jest telewizor niemieckiej marki MEDION o doskonałej jakości obrazu. " +
				"Telewizor można podpinać zarówno do komputera jak i innych urządzeń multimedialnych. " +
				"Telewizor wyświetla obraz w pełnej rozdzielczości Full HD (1920 x 1080 pixeli)." +
				" Ponadto telewizor jest przystosowany do powieszenia na ścianie (VESA 200 x 200). MEDION ® Life wyświetla ostry obraz i żywe kolory" +
				".Ponadto urządzenie posiada 3 gniazda HDMI z HDCP oraz 2 gniazda USB,co umożliwia oglądanie plików " +
				"MP3,JPG,WMA,MPEG1 / 2 / 4,MKV MPEG4,Xvid z nośników.Prezentowany model posiada wbudowany tuner DVB - T / T2 MPEG - 4," +
				"dzięki czemu odbiera naziemną telewizję cyfrową bez dodatkowych dekoderów.Ponadto telewizor posiada wbudowane tunery DVB - S2" +
				" oraz DVB - C(tuner satelitarny oraz kablowy).",
				Price = 679.00,
				ImagePath = _destinationPath + "MedionTV.png",
				Quantity = 16,
				Categories = new List<Category> { electronicCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 5,
				Brand = "Kiano",
				Name = "Telewizor KIANO Slim TV 50 cali Full HD dD+ FHDpro",
				Description = "W formacie Full HD: szczegółowo i realistycznie. Każdy szczegół Twoich ulubionych scen " +
				"czy akcji piłkarskich widoczny jak na dłoni w rozdzielczości Full HD 1920x1080 Mpx.Wybierz jeden z dostępnych " +
				".Trybów obrazu: (dynamiczny, standardowy, łagodny i własny), do tego ustaw ulubione odcienie barw obrazu - " +
				"Wybór barw: Standardowe, Chłodne i Ciepłe i ciesz się nieskończoną ilością emocji.FHDpro Reality Look PRO. " +
				"Dostrzeż różnice w szczegółach obrazu dzięki technologii FHD Reality Look PRO. Prezentujemy niezwykły świat obrazu " +
				"o niespotykanej czystości. Technologia FHD Reality Look PRO analizuje wyświetlane elementy obrazu i w czasie" +
				" rzeczywistym poddaje je analizie i automatycznie przetwarza je,optymalizując faktury kształtów,kolory i krawędzie.",
				Price = 1099.00,
				ImagePath = _destinationPath + "KianoTV.png",
				Quantity = 75,
				Categories = new List<Category> { electronicCategory }
			});

			_db.Products.AddOrUpdate(new Product
			{
				Id = 6,
				Brand = "Fiorucci",
				Name = "OUTLET! Mokasyny Damskie Fiorucci R: 39 OKAZJA!",
				Description = "Boisz się kupować przez internet? Strach przed podróbkami? " +
				"Z nami możesz czuć się bezpiecznie.Jesteśmy sklepem z punktami stacjonarnymi.Każdy nasz produkt jest w 100 % oryginalny!" +
				"Ze względu na to,że sprzedaż prowadzimy również w sklepach stacjonarnych, zastrzegamy sobie możliwość wcześniejszego " +
				"zakończenia aukcji lub zmiany dostępnej ilości. ",
				Price = 79.99,
				ImagePath = _destinationPath + "Mokasyny.png",
				Quantity = 29,
				Categories = new List<Category> { fashionCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 7,
				Brand = "Fiorucci",
				Name = "OUTLET! Mokasyny Damskie Fiorucci R: 39 OKAZJA!",
				Description = "Boisz się kupować przez internet? Strach przed podróbkami? " +
				"Z nami możesz czuć się bezpiecznie.Jesteśmy sklepem z punktami stacjonarnymi.Każdy nasz produkt jest w 100 % oryginalny!" +
				"Ze względu na to,że sprzedaż prowadzimy również w sklepach stacjonarnych, zastrzegamy sobie możliwość wcześniejszego " +
				"zakończenia aukcji lub zmiany dostępnej ilości. ",
				Price = 79.99,
				ImagePath = _destinationPath + "Mokasyny.png",
				Quantity = 29,
				Categories = new List<Category> { fashionCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 8,
				Brand = "Aldo",
				Name = "Mokasyny męskie Aldo z naturalnej skóry 41",
				Description = "Cena sklepowa: 550 zł. Nasza cena: 129.00(-77 %). Produkt oryginalny,nowy,sprzedawany bez metki " +
				"i bez pudełka,może nosić ślady przymierzania.",
				Price = 129.00,
				ImagePath = _destinationPath + "Mokasyny2.png",
				Quantity = 1,
				Categories = new List<Category> { fashionCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 9,
				Brand = "Inna marka",
				Name = "KOSZULKA T-SHIRT BAWEŁNA FULLPRINT 3D ALIEN OBCY",
				Description = "Koszulka wykonana z wysokogatunkowej dzianiny .Dzianina oddychająca i elastyczna." +
				"Koszulka nie mechaci się i nie kulkuje. Skład: 50 % bawełna(strona wewnętrzna - od skóry)," +
				"50 % poliester(strona wierzchnia - nadrukowana)Wysokiej jakości nadruk wykonywany specjalistyczną metodą " +
				"jest integralną częścią koszulki(niewyczuwalny w dotyku) przez co charakteryzuje się niezwykłą trwałością,nie blaknie," +
				"nie ulega zniszczeniu z biegiem czasu oraz zapewnia żywe i nasycone kolory.Produkt  fabrycznie nowy. " +
				"Bardzo dobra jakość.. ",
				Price = 40.00,
				ImagePath = _destinationPath + "Koszulka.png",
				Quantity = 99,
				Categories = new List<Category> { fashionCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 10,
				Brand = "Inna marka",
				Name = "BLUZA FULLPRINT 3D BAWEŁNA NADRUK SILNIK MOTOCYKL",
				Description = "Bluza wykonana z wysokogatunkowej dzianiny.Dzianina oddychająca i elastyczna." +
				"Bluza nie mechaci się i nie kulkuje.Skład:50 % bawełna(strona wewnętrzna - od skóry)50 % poliester" +
				"(strona wierzchnia - nadrukowana)Wysokiej jakości nadruk wykonywany specjalistyczną metodą jest" +
				" integralną częścią bluzy(niewyczuwalny w dotyku) przez co charakteryzuje się niezwykłą trwałością,nie blaknie," +
				"nie ulega zniszczeniu z biegiem czasu oraz zapewnia żywe i nasycone kolory." +
				"Bluza posiada ściągacze przy rękawach i na dole.Produkt fabrycznie nowy.Bardzo dobra jakość. ",
				Price = 65.00,
				ImagePath = _destinationPath + "Bluza.png",
				Quantity = 100,
				Categories = new List<Category> { fashionCategory }
			});

			_db.Products.AddOrUpdate(new Product
			{
				Id = 11,
				Brand = "Renault",
				Name = "RENAULT TWINGO 2009 r.",
				Description = "Renault Twingo z roku 2009 . przywiezione z Francji. Przebieg 187000, diesel." +
				"Auto jeżdżące do drobnych poprawek lakierniczych. " +
				"Radio, elektryka , klima manual. Cena + opłaty.Zapraszam do zakupu",
				Price = 5200.00,
				ImagePath = _destinationPath + "Twingo.png",
				Quantity = 1,
				Categories = new List<Category> { carCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 12,
				Brand = "Renault",
				Name = "Toyota Avensis III Benzyna, 2012",
				Description = "Toyota Avensis 1.8 benzyna 102 tys km .Perfekcyjnie utrzymana. Potwierdzenie przebiegu," +
				" ksiazka serwisowa serwis Toyoty oraz elektroniczne potwierdzenie przebiegu cepik Wloski do wgladu na miejscu. " +
				"Jeden wlasciciel. Auto w perfekcyjnym stanie pod kazdym wzgledem. Dwa komplety opon. Pelne wyposazenie nawigacja," +
				"kamera skóra ,itd . Zarejestrowana, ubezpieczona wymienione, filtry, olej. Kola 17 cali.Ogloszenie prywatne",
				Price = 24500.00,
				ImagePath = _destinationPath + "Avenis.png",
				Quantity = 1,
				Categories = new List<Category> { carCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 13,
				Brand = "Chevrolet",
				Name = "Chevrolet Camaro Zielone 3.6 Felgi 22 Chrom",
				Description = "Chevrolet Camaro Zielone. Rok produkcji 2010, przebieg 160000 mil, 3600cm^3, 300KM," +
				" ABS, ASR (kontrola trakcji), ESP (stabilizacja toru jazdy)," +
				" Immobilizer, Poduszka powietrzna kierowcy, Poduszka powietrzna pasażera, Poduszki boczne przednie, " +
				"Poduszki boczne tylne, Kurtyny powietrzne, Elektryczne szyby przednie, Tapicerka tekstylna, Wspomaganie kierownicy",
				Price = 41900.00,
				ImagePath = _destinationPath + "Camaro.png",
				Quantity = 1,
				Categories = new List<Category> { carCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 14,
				Brand = "Lamborghini",
				Name = "Lamborghini Aventador",
				Description = "Oficjalny dealer Ferrari - Ferrari Katowice prezentuje jedyny w Polsce egzemplarz Lamborghini " +
	"Aventador SV Roadster. ### DANE PODSTAWOWE: - Kolor zewnętrzny: Verde Ithaca - Kolor tapicerki: Nero (alcantara) WYPOSAŻENIE " +
 "STANDARDOWE: - ABS - Airbag kierowcy i pasażera - Elektrycznie regulowane szyby – przód - Sportowe fotele przednie - Ceramiczne " +
 "hamulce - Immobilizer - Klimatyzacja automatyczna - Komputer pokładowy - Reflektory bi-ksenonowe ze spryskiwaczami - Lusterka boczne" +
 " regulowane elektrycznie z funkcją podgrzewania - Lusterka boczne składane elektrycznie - Radio CD/MP3 - System kontroli trakcji -" +
 " System nawigacji satelitarnej - Wspomaganie układu kierowniczego - Zamek centralny z pilotem - Przycisk START na konsoli centralnej" +
 " -Trzy tryby jazdy: Strada, Sport i Corsa WYPOSAŻENIE DODATKOWE: -Przezroczysta pokrywa komory silnika -Karbonowe wloty powietrza –" +
 " tył -Małe logo SV (naklejka) -Hardtop w kolorze czarnym -Felgi DIONE 20/21-cali w kolorze czarnym z oponami Pirelli P Zero Corsa " +
 "Sport -Zawieszenie magnetoreologiczne -System podnoszonego przedniego zawieszenia -Czarne zaciski hamulcowe -Sensonum – opcjonalny " +
 "system Hi-Fi premium -Kamera cofania -Elektrycznie regulowane oraz podgrzewane fotele z elementami karbonowymi -Pakiet Branding " +
 "(alcantara) -Kierownica z alcantary -Pakiet Travel (1 uchwyt na napoje, 1 schowek z siatki na drobiazgi) -Karbonowe dywaniki " +
 "-Opcjonalny pakiet Lamborghini Sound & Performance, zmieniający charakterystykę pracy układu wydechowego w trybach Sport i " +
 "Corsa Oferowany egzemplarz pochodzi z limitowanej edycji 500 sztuk. Obecnie jest to jedyny Aventador SV Roadster zarejestrowany" +
 " w Polsce. Został kupiony u autoryzowanego dealera marki Lamborghini w Warszawie i jest objęty fabryczną gwarancją producenta." +
 " Cena brutto zawiera akcyzę oraz 23% VAT (do odliczenia 50% lub całość podatku VAT). ### O NAS Ferrari Katowice to oficjalny " +
 "dealer samochodów Ferrari w Polsce. Po latach starań, dealerstwo zostało otwarte w maju 2013 roku i tym samym stało się wizytówką" +
 " Grupy Pietrzak- rodzinnego biznesu, który w świecie motoryzacji znany jest od ponad 20 lat. Ferrari Katowice to showroom z " +
 "nowymi i używanymi samochodami Ferrari, ale także autoryzowany serwis z profesjonalną załogą, która od lat pracuje z markami luksusowymi",
				Price = 2500000.00,
				ImagePath = _destinationPath + "Aventador.png",
				Quantity = 1,
				Categories = new List<Category> { carCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 15,
				Brand = "Maserati",
				Name = "Maserati Gran Cabrio Sport 4.7 V8 stan idealny ASO",
				Description = "Witam, mam na sprzedaż Maserati Gran Cabrio Sport po liftingu. Autko w perfekcyjnym stanie," +
				" świeżo po pełnym serwisie w Maserati Chodzeń w Warszawie 22.03.2019r . Autko posiadam od 2 lat." +
				" Wyrażam zgodę na wizytę w serwisie.08.04.2019r auta zostało poddane pełnemu car detailingu,zabezpieczenie" +
				" skóry,zabezpieczenie dachu oraz została nałożona ceramika",
				Price = 359000.00,
				ImagePath = _destinationPath + "GranCabrio.png",
				Quantity = 1,
				Categories = new List<Category> { carCategory }
			});

			_db.Products.AddOrUpdate(new Product
			{
				Id = 16,
				Brand = "Inna marka",
				Name = "ROLLER JOGA WAŁEK DO MASAŻU CROSSFIT FITNESS",
				Description = "Zamień kosztowne wizyty u fizjoterapeuty na masaż w domowym zaciszu dzięki naszemu rollerowi." +
				" Wałek wykonany jest z elastycznej, choć jednocześnie trwałej pianki EVA. Środek jest dodatkowo wzmocniony twardym rdzeniem" +
				" z tworzywa termoplastycznego. Całość wygląda niezwykle nowocześnie, a duży wybór kolorów pozwoli każdemu dostosować wałek do" +
				" własnych preferencji kolorystycznych.Największymi zaletami rollera są: wytrzymałość nawet przy najbardziej wymagających ćwiczeniach" +
				",trwałość pozwalająca cieszyć się z użytkowania produktu przez lata,gładkość zapewniająca ochronę przed zabrudzeniami i bakteriami." +
				"Ergonomiczne wypustki masujące intensyfikują doznania podczas masażu i ćwiczeń,stwarzając wrażenie nacisku palców masażysty oraz" +
				" pomagając w rozbijaniu związanej tkanki mięśniowej.",
				Price = 19.00,
				ImagePath = _destinationPath + "Roller.png",
				Quantity = 9421,
				Categories = new List<Category> { sportCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 17,
				Brand = "Jump fit",
				Name = "ZESTAW 3 GUM TAŚM DO ĆWICZEŃ CROSSFIT FITNESS",
				Description = "Gumy do ćwiczeń marki JUMPFIT zostały wykonane ze specjalistycznej gumy lateksowej o " +
				"podwyższonych parametrach wytrzymałościowych. Gumy umożliwiają wszechstronny trening wszystkich partii ciała. Gumy treningowe o zamkniętym " +
				"obwodzie doskonale sprawdzą się w ćwiczeniach ogólnorozwojowych, rozciągających, siłowych, crossfit, rehabilitacyjnych. Będą stanowić idealny " +
				"dodatek do treningu aerobowego. Regularny trening z gumami Jumpfit skutecznie wzmacnia mięśnie, poprawia wytrzymałość, zwiększa elastyczność stawów " +
				"i pomaga spalić zbędną tkankę tłuszczową.",
				Price = 64.00,
				ImagePath = _destinationPath + "Gumy.png",
				Quantity = 9931,
				Categories = new List<Category> { sportCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 18	,
				Brand = "JET5",
				Name = "JET5 DUŻA METALOWA BRAMKA PIŁKARSKA 213x150 SIATKA",
				Description = "Zestaw piłkarski treningowy dla dzieci, młodzieży i dorosłych. Zestaw składa się z wysokiej jakości" +
				" elementów z których zmontujesz metalową bramkę. Łatwy montaż.Bramka ma 90 cm głębokości, co czyni ja bardziej stabilną i bezpieczną," +
				" w odróżnieniu od podobnych bramek na rynku, które posiadają 75 cm głębokości.Zestaw piłkarski to prawdziwa atrakcja, przygotowana dla najmłodszych" +
				" fanów piłki nożnej. Zestaw składa się z wysokiej jakości bramki i siatki. Świetny pomysł dla dzieci, aby aktywnie spędzały czas na świeżym powietrzu." +
				"Idealna do gry w ogrodzie, na boisku, na polanie...",
				Price = 119.00,
				ImagePath = _destinationPath + "Bramka.png",
				Quantity = 121,
				Categories = new List<Category> { sportCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 19,
				Brand = "Nike",
				Name = "Piłka nożna Nike Pitch Team SC3166 Zielona Roz 5",
				Description = "Piłka NIKE Pitch Team w rozmiarze 5. " +
				"Piłka składa się z kilku warstw szytych maszynowo łat z wysokiej jakości materiału, " +
				"co gwarantuje doskonałą wytrzymałość oraz stabilny lot,z kolei intensywna kolorystyka" +
				" znacznie zwiększa widoczność jej podczas złych warunków.",
				Price = 49.00,
				ImagePath = _destinationPath + "Pilka.png",
				Quantity = 82,
				Categories = new List<Category> { sportCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 20,
				Brand = "Inna Marka",
				Name = "HULAJNOGA STUNT WYCZYNOWA 100kg SCOOTER",
				Description = "Hulajnoga wyczynowa STUNT doskonale nadaje się do nauki tricków i ewolucji. " +
				"Posiada wzmocniony podest z aluminium 6061 , kierownicę 360° ze stali chromowanej, która jest bardziej sztywna niż aluminiowa" +
				", najwyższej klasy łożyska ABEC-9 Carbon oraz system hamulcowy FLEX FENDER. Podest i rama hulajnogi STUNT w 100% wykonane są z aluminium" +
				", co gwarantuje dużą wytrzymałość i trwałość łączeń.Zarówno podest, jak poprzeczna rama łącząca wykonane są w 100% " +
				"z aluminium. Wykorzystanie tego samego materiału w podeście i ramie jest pewnością najtrwalszego połączenia. Pamiętaj, że hulajnoga" +
				" wyczynowa narażona jest na duże przeciążenia. Niektóre hulajnogi posiadają konstrukcję aluminiowo-stalową, która nie zapewnia odpowiedniej" +
				" trwałości łączeń ze względu na kruchość spoin.",
				Price = 185.00,
				ImagePath = _destinationPath + "Scooter.png",
				Quantity = 12,
				Categories = new List<Category> { sportCategory }
			});

			_db.Products.AddOrUpdate(new Product
			{
				Id = 21,
				Brand = "AL-KO",
				Name = "Kosiarka spalinowa z napędem ALKO Easy 5.1 SP-S",
				Description = "Na tej aukcji kupujesz oryginalną kosiarkę spalinową z pełnym wyposażeniem, w którym znajduje się:" +
				"Kosiarka spalinowa niemieckiej marki AL-KO 5.1 SP-S Easy, Oryginalna wkładka mulczująca AL-KO, " +
				"Oryginalna wkładka wyrzutu bocznego AL-KO,Oryginalny zestaw montażowy AL-KO: motylki, śrubki, podkładki" +
				"Nowoczesna i wygodna, taka jest kosiarka spalinowa AL-KO 5.1 SP-S Easy. Zaprojektowana w Niemczech i wyprodukowana w Austrii" +
				" kosiarka ta jest najwyższej jakości. Jej nowoczesny design sprawia, że przyciąga wzrok w każdym ogrodzie, a zastosowane komponenty " +
				"konstrukcyjne zwiększają żywotność kosiarki. Użytkowanie kosiarki Easy sprawia, że koszenie trawy staję się przyjemnością.",
				Price = 1199.00,
				ImagePath = _destinationPath + "Kosiarka.png",
				Quantity = 12,
				Categories = new List<Category> { houseCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 22,
				Brand = "AL-KO",
				Name = "Kosiarka spalinowa z napędem ALKO Easy 5.1 SP-S",
				Description = "Na tej aukcji kupujesz oryginalną kosiarkę spalinową z pełnym wyposażeniem, w którym znajduje się:" +
				"Kosiarka spalinowa niemieckiej marki AL-KO 5.1 SP-S Easy, Oryginalna wkładka mulczująca AL-KO, " +
				"Oryginalna wkładka wyrzutu bocznego AL-KO,Oryginalny zestaw montażowy AL-KO: motylki, śrubki, podkładki" +
				"Nowoczesna i wygodna, taka jest kosiarka spalinowa AL-KO 5.1 SP-S Easy. Zaprojektowana w Niemczech i wyprodukowana w Austrii" +
				" kosiarka ta jest najwyższej jakości. Jej nowoczesny design sprawia, że przyciąga wzrok w każdym ogrodzie, a zastosowane komponenty " +
				"konstrukcyjne zwiększają żywotność kosiarki. Użytkowanie kosiarki Easy sprawia, że koszenie trawy staję się przyjemnością.",
				Price = 1199.00,
				ImagePath = _destinationPath + "Kosiarka.png",
				Quantity = 53,
				Categories = new List<Category> { houseCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 23,
				Brand = "AL-KO",
				Name = "Kosiarka spalinowa z napędem ALKO Easy 5.1 SP-S",
				Description = "Sydney Silver - zestaw ogrodowy rozkładany aluminiowy to idealna propozycja dla osób, " +
				"które uwielbiają spożywać ciepłe posiłki na świeżym powietrzu. Składa się on z sześciu pozycjonowanych i rozkładanych krzeseł " +
				"oraz dużego, prostokątnego stołu.Sydney Silver - zestaw ogrodowy rozkładany aluminiowy wyróżnia się " +
				"niebanalnym wyglądem. Połączenie koloru taupe, który widoczny jest na siedziskach, oparciach czy na blacie przestronnego " +
				"stołu, z matową szarością aluminium stelaża, to kompozycja spójna, ciekawa, nowoczesna.",
				Price = 1199.00,
				ImagePath = _destinationPath + "Stol.png",
				Quantity = 105,
				Categories = new List<Category> { houseCategory }
			});



			_db.SaveChanges();
		}
	}
}
