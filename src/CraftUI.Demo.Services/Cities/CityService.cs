using CraftUI.Demo.Application.Cities;
using CraftUI.Demo.Application.Common.Interfaces.Services;

namespace CraftUI.Demo.Services.Cities;

public class CityService : ICityService
{
    private readonly List<CityVm> _cities =
    [
        // France
        new (1, 1, "Paris"),
        new (2, 1, "Lyon"),
        new (3, 1, "Marseille"),
        new (4, 1, "Toulouse"),
        new (5, 1, "Nice"),

        // United States
        new (6, 2, "New York"),
        new (7, 2, "Los Angeles"),
        new (8, 2, "Chicago"),
        new (9, 2, "Houston"),
        new (10, 2, "Phoenix"),

        // Japan
        new (11, 3, "Tokyo"),
        new (12, 3, "Osaka"),
        new (13, 3, "Kyoto"),
        new (14, 3, "Nagoya"),
        new (15, 3, "Fukuoka"),

        // United Kingdom
        new (16, 4, "London"),
        new (17, 4, "Manchester"),
        new (18, 4, "Birmingham"),
        new (19, 4, "Glasgow"),
        new (20, 4, "Liverpool"),

        // Australia
        new (21, 5, "Sydney"),
        new (22, 5, "Melbourne"),
        new (23, 5, "Brisbane"),
        new (24, 5, "Perth"),
        new (25, 5, "Adelaide"),

        // Canada
        new (26, 6, "Toronto"),
        new (27, 6, "Vancouver"),
        new (28, 6, "Montreal"),
        new (29, 6, "Calgary"),
        new (30, 6, "Ottawa"),

        // Germany
        new (31, 7, "Berlin"),
        new (32, 7, "Munich"),
        new (33, 7, "Frankfurt"),
        new (34, 7, "Hamburg"),
        new (35, 7, "Cologne"),

        // Italy
        new (36, 8, "Rome"),
        new (37, 8, "Milan"),
        new (38, 8, "Naples"),
        new (39, 8, "Turin"),
        new (40, 8, "Florence"),

        // Spain
        new (41, 9, "Madrid"),
        new (42, 9, "Barcelona"),
        new (43, 9, "Valencia"),
        new (44, 9, "Seville"),
        new (45, 9, "Bilbao"),

        // Brazil
        new (46, 10, "São Paulo"),
        new (47, 10, "Rio de Janeiro"),
        new (48, 10, "Brasília"),
        new (49, 10, "Salvador"),
        new (50, 10, "Fortaleza"),

        // China
        new (51, 11, "Beijing"),
        new (52, 11, "Shanghai"),
        new (53, 11, "Guangzhou"),
        new (54, 11, "Shenzhen"),
        new (55, 11, "Chengdu"),

        // India
        new (56, 12, "Mumbai"),
        new (57, 12, "Delhi"),
        new (58, 12, "Bangalore"),
        new (59, 12, "Kolkata"),
        new (60, 12, "Chennai"),

        // Mexico
        new (61, 13, "Mexico City"),
        new (62, 13, "Guadalajara"),
        new (63, 13, "Monterrey"),
        new (64, 13, "Puebla"),
        new (65, 13, "Tijuana"),

        // Russia
        new (66, 14, "Moscow"),
        new (67, 14, "Saint Petersburg"),
        new (68, 14, "Novosibirsk"),
        new (69, 14, "Yekaterinburg"),
        new (70, 14, "Kazan"),

        // South Korea
        new (71, 15, "Seoul"),
        new (72, 15, "Busan"),
        new (73, 15, "Incheon"),
        new (74, 15, "Daegu"),
        new (75, 15, "Daejeon"),

        // Netherlands
        new (76, 16, "Amsterdam"),
        new (77, 16, "Rotterdam"),
        new (78, 16, "The Hague"),
        new (79, 16, "Utrecht"),
        new (80, 16, "Eindhoven"),

        // Sweden
        new (81, 17, "Stockholm"),
        new (82, 17, "Gothenburg"),
        new (83, 17, "Malmö"),
        new (84, 17, "Uppsala"),
        new (85, 17, "Västerås"),

        // Norway
        new (86, 18, "Oslo"),
        new (87, 18, "Bergen"),
        new (88, 18, "Stavanger"),
        new (89, 18, "Trondheim"),
        new (90, 18, "Drammen"),

        // Denmark
        new (91, 19, "Copenhagen"),
        new (92, 19, "Aarhus"),
        new (93, 19, "Odense"),
        new (94, 19, "Aalborg"),

        // Finland
        new (95, 20, "Helsinki"),
        new (96, 20, "Espoo"),
        new (97, 20, "Tampere"),
        new (98, 20, "Vantaa"),
        new (99, 20, "Oulu"),

        // Argentina
        new (100, 21, "Buenos Aires"),
        new (101, 21, "Córdoba"),
        new (102, 21, "Rosario"),
        new (103, 21, "Mendoza"),
        new (104, 21, "La Plata"),

        // Belgium
        new (105, 22, "Brussels"),
        new (106, 22, "Antwerp"),
        new (107, 22, "Ghent"),
        new (108, 22, "Bruges"),
        new (109, 22, "Leuven"),

        // Chile
        new (110, 23, "Santiago"),
        new (111, 23, "Valparaíso"),
        new (112, 23, "Concepción"),

        // Colombia
        new (113, 24, "Bogotá"),
        new (114, 24, "Medellín"),
        new (115, 24, "Cali"),

        // New Zealand
        new (116, 25, "Auckland"),
        new (117, 25, "Wellington"),
        new (118, 25, "Christchurch"),

        // South Africa
        new (119, 26, "Johannesburg"),
        new (120, 26, "Cape Town"),
        new (121, 26, "Durban"),

        // Ireland
        new (122, 27, "Dublin"),
        new (123, 27, "Cork"),
        new (124, 27, "Limerick"),

        // Portugal
        new (125, 28, "Lisbon"),
        new (126, 28, "Porto"),
        new (127, 28, "Braga"),

        // Greece
        new (128, 29, "Athens"),
        new (129, 29, "Thessaloniki"),
        new (130, 29, "Patras"),

        // Turkey
        new (131, 30, "Istanbul"),
        new (132, 30, "Ankara"),
        new (133, 30, "Izmir"),

        // Saudi Arabia
        new (134, 31, "Riyadh"),
        new (135, 31, "Jeddah"),
        new (136, 31, "Mecca"),

        // Israel
        new (137, 32, "Tel Aviv"),
        new (138, 32, "Jerusalem"),
        new (139, 32, "Haifa"),

        // Poland
        new (140, 33, "Warsaw"),
        new (141, 33, "Krakow"),
        new (142, 33, "Wrocław"),

        // Switzerland
        new (143, 34, "Zurich"),
        new (144, 34, "Geneva"),
        new (145, 34, "Basel"),

        // Austria
        new (146, 35, "Vienna"),
        new (147, 35, "Salzburg"),
        new (148, 35, "Innsbruck"),

        // Hungary
        new (149, 36, "Budapest"),
        new (150, 36, "Debrecen"),
        new (151, 36, "Szeged"),

        // Czech Republic
        new (152, 37, "Prague"),
        new (153, 37, "Brno"),
        new (154, 37, "Ostrava"),

        // Ukraine
        new (155, 38, "Kyiv"),
        new (156, 38, "Lviv"),
        new (157, 38, "Odessa"),

        // Vietnam
        new (158, 39, "Hanoi"),
        new (159, 39, "Ho Chi Minh City"),
        new (160, 39, "Da Nang"),

        // Thailand
        new (161, 40, "Bangkok"),
        new (162, 40, "Chiang Mai"),
        new (163, 40, "Phuket"),

        // Malaysia
        new (164, 41, "Kuala Lumpur"),
        new (165, 41, "George Town"),
        new (166, 41, "Ipoh"),

        // Philippines
        new (167, 42, "Manila"),
        new (168, 42, "Cebu"),
        new (169, 42, "Davao"),

        // Indonesia
        new (170, 43, "Jakarta"),
        new (171, 43, "Surabaya"),
        new (172, 43, "Bandung"),

        // Singapore
        new (173, 44, "Singapore"),

        // Pakistan
        new (174, 45, "Karachi"),
        new (175, 45, "Lahore"),
        new (176, 45, "Islamabad"),

        // Bangladesh
        new (177, 46, "Dhaka"),
        new (178, 46, "Chittagong"),
        new (179, 46, "Khulna"),

        // Sri Lanka
        new (180, 47, "Colombo"),
        new (181, 47, "Kandy"),
        new (182, 47, "Galle"),

        // Nepal
        new (183, 48, "Kathmandu"),
        new (184, 48, "Pokhara"),
        new (185, 48, "Lalitpur"),

        // Afghanistan
        new (186, 49, "Kabul"),
        new (187, 49, "Herat"),
        new (188, 49, "Kandahar"),

        // Kazakhstan
        new (189, 50, "Almaty"),
        new (190, 50, "Nur-Sultan"),
        new (191, 50, "Shymkent"),

        // Uzbekistan
        new (192, 51, "Tashkent"),
        new (193, 51, "Samarkand"),
        new (194, 51, "Bukhara"),

        // Azerbaijan
        new (195, 52, "Baku"),
        new (196, 52, "Ganja"),
        new (197, 52, "Sumqayit"),

        // Armenia
        new (198, 53, "Yerevan"),
        new (199, 53, "Gyumri"),
        new (200, 53, "Vanadzor"),

        // Georgia
        new (201, 54, "Tbilisi"),
        new (202, 54, "Batumi"),
        new (203, 54, "Kutaisi"),

        // Morocco
        new (204, 55, "Casablanca"),
        new (205, 55, "Marrakesh"),
        new (206, 55, "Rabat"),
        new (207, 55, "Fes"),
        new (208, 55, "Tangier"),

        // Tunisia
        new (209, 56, "Tunis"),
        new (210, 56, "Sfax"),
        new (211, 56, "Sousse"),

        // Egypt
        new (212, 57, "Cairo"),
        new (213, 57, "Alexandria"),
        new (214, 57, "Giza"),
        new (215, 57, "Shubra El Kheima"),
        new (216, 57, "Port Said"),

        // Kenya
        new (217, 58, "Nairobi"),
        new (218, 58, "Mombasa"),
        new (219, 58, "Kisumu"),

        // Nigeria
        new (220, 59, "Lagos"),
        new (221, 59, "Abuja"),
        new (222, 59, "Kano"),
        new (223, 59, "Ibadan"),
        new (224, 59, "Port Harcourt"),

        // Ghana
        new (225, 60, "Accra"),
        new (226, 60, "Kumasi"),
        new (227, 60, "Tamale"),

        // Ivory Coast
        new (228, 61, "Abidjan"),
        new (229, 61, "Bouaké"),
        new (230, 61, "Daloa"),

        // Senegal
        new (231, 62, "Dakar"),
        new (232, 62, "Touba"),
        new (233, 62, "Thiès"),

        // Uganda
        new (234, 63, "Kampala"),
        new (235, 63, "Gulu"),
        new (236, 63, "Lira"),

        // Ethiopia
        new (237, 64, "Addis Ababa"),
        new (238, 64, "Dire Dawa"),
        new (239, 64, "Mekelle"),

        // Tanzania
        new (240, 65, "Dar es Salaam"),
        new (241, 65, "Dodoma"),
        new (242, 65, "Mwanza"),

        // Zambia
        new (243, 66, "Lusaka"),
        new (244, 66, "Kitwe"),
        new (245, 66, "Ndola"),

        // Zimbabwe
        new (246, 67, "Harare"),
        new (247, 67, "Bulawayo"),
        new (248, 67, "Mutare"),

        // Algeria
        new (249, 68, "Algiers"),
        new (250, 68, "Oran"),
        new (251, 68, "Constantine"),

        // Libya
        new (252, 69, "Tripoli"),
        new (253, 69, "Benghazi"),
        new (254, 69, "Misrata"),

        // Sudan
        new (255, 70, "Khartoum"),
        new (256, 70, "Omdurman"),
        new (257, 70, "Port Sudan"),

        // Angola
        new (258, 71, "Luanda"),
        new (259, 71, "Huambo"),
        new (260, 71, "Lobito"),

        // Mozambique
        new (261, 72, "Maputo"),
        new (262, 72, "Matola"),
        new (263, 72, "Beira"),

        // Botswana
        new (264, 73, "Gaborone"),
        new (265, 73, "Francistown"),

        // Namibia
        new (266, 74, "Windhoek"),
        new (267, 74, "Walvis Bay"),

        // Madagascar
        new (268, 75, "Antananarivo"),
        new (269, 75, "Toamasina"),
        new (270, 75, "Antsirabe"),

        // Mauritius
        new (271, 76, "Port Louis"),
        new (272, 76, "Curepipe"),

        // Seychelles
        new (273, 77, "Victoria"),

        // Malawi
        new (274, 78, "Lilongwe"),
        new (275, 78, "Blantyre"),

        // Rwanda
        new (276, 79, "Kigali"),
        new (277, 79, "Butare"),

        // Burundi
        new (278, 80, "Bujumbura"),
        new (279, 80, "Gitega"),

        // Mali
        new (280, 81, "Bamako"),
        new (281, 81, "Sikasso"),

        // Niger
        new (282, 82, "Niamey"),
        new (283, 82, "Zinder"),

        // Chad
        new (284, 83, "N'Djamena"),
        new (285, 83, "Moundou"),

        // Central African Republic
        new (286, 84, "Bangui"),

        // Democratic Republic of the Congo
        new (287, 85, "Kinshasa"),
        new (288, 85, "Lubumbashi"),
        new (289, 85, "Mbuji-Mayi"),

        // Republic of the Congo
        new (290, 86, "Brazzaville"),
        new (291, 86, "Pointe-Noire"),

        // Gabon
        new (292, 87, "Libreville"),
        new (293, 87, "Port-Gentil"),

        // Equatorial Guinea
        new (294, 88, "Malabo"),
        new (295, 88, "Bata"),

        // Sao Tome and Principe
        new (296, 89, "São Tomé"),

        // Togo
        new (297, 90, "Lomé"),
        new (298, 90, "Sokodé"),

        // Benin
        new (299, 91, "Cotonou"),
        new (300, 91, "Porto-Novo"),

        // Sierra Leone
        new (301, 92, "Freetown"),
        new (302, 92, "Bo"),

        // Liberia
        new (303, 93, "Monrovia"),
        new (304, 93, "Gbarnga"),

        // Cameroon
        new (305, 94, "Yaoundé"),
        new (306, 94, "Douala"),

        // Burkina Faso
        new (307, 95, "Ouagadougou"),
        new (308, 95, "Bobo-Dioulasso"),

        // Lesotho
        new (309, 96, "Maseru"),

        // Eswatini (Swaziland)
        new (310, 97, "Mbabane"),
        new (311, 97, "Manzini"),

        // Somalia
        new (312, 98, "Mogadishu"),
        new (313, 98, "Hargeisa"),

        // Djibouti
        new (314, 99, "Djibouti City"),

        // Eritrea
        new (315, 100, "Asmara")
    ];
    
    public async Task<IReadOnlyList<CityVm>> GetAllCitiesAsync(CancellationToken cancellationToken = default)
    {
        //await Task.Delay(2000, cancellationToken);
        
        return _cities.AsReadOnly();
    }

    public async Task<IReadOnlyList<CityVm>> GetCitiesAsync(int countryId, CancellationToken cancellationToken = default)
    {
        //await Task.Delay(2000, cancellationToken);
        
        return _cities.Where(x => x.CountryId == countryId).ToList().AsReadOnly();
    }
}