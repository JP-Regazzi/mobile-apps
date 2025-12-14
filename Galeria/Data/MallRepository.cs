using Galeria.Models;

namespace Galeria.Data;

public static class MallRepository
{
    public static IList<Store> GetStores()
    {
        return new List<Store>
        {
            new Store
            {
                Name = "BURGER KING",
                Type = StoreType.Restaurant,
                Logo = "store_burgerking_logo.png",
                Banner = "store_burgerking_header.png",
                ShortDescription = "Burger King, muitas vezes abreviado como BK, é uma rede de...",
                FullDescription = "Burger King, muitas vezes abreviado como BK, é uma rede de restaurantes especializada em fast-food, fundada nos Estados Unidos por James McLamore e David Edgerton, que abriram a primeira unidade em Miami, Flórida.",
                Location = "3 Andar - Loja 10 - Setor Norte",
                Phone = "(61) 3333-3333"
            },
            new Store
            {
                Name = "RENNER",
                Type = StoreType.Store,
                Logo = "store_renner_logo.png",
                Banner = "store_renner_header.png",
                ShortDescription = "A Lojas Renner S.A. é uma rede de lojas de departamento brasileira...",
                FullDescription = "A Lojas Renner S.A. teve seu início em 1922, com o começo das atividades fabris do então Grupo A.J. Renner, e desvinculou-se do grupo somente em 1965, quando suas lojas começaram a tomar um formato mais próximo do atual.",
                Location = "3 Andar - Loja 10 - Setor Norte",
                Phone = "(61) 3333-3333"
            },
            new Store
            {
                Name = "SEPHORA",
                Type = StoreType.Store,
                Logo = "store_sephora_logo.png",
                Banner = "store_sephora_header.png",
                ShortDescription = "Cosméticos, perfumes e maquiagens das principais marcas.",
                FullDescription = "A Sephora é uma rede internacional de cosméticos que reúne perfumes, maquiagens, produtos para pele e cabelo, com marcas próprias e grifes consagradas. Na unidade do shopping, o cliente encontra atendimento especializado, ambientes para experimentação e lançamentos exclusivos ao longo do ano.",
                Location = "2 Andar - Loja 25 - Corredor Leste",
                Phone = "(61) 3555-0123"
            },
            new Store
            {
                Name = "GENDAI",
                Type = StoreType.Restaurant,
                Logo = "store_gendai_logo.png",
                Banner = "store_gendai_header.png",
                ShortDescription = "Culinária japonesa com sushis, temakis e pratos quentes.",
                FullDescription = "O Gendai é uma rede de restaurantes de culinária japonesa que oferece sushis, temakis, combinados e pratos quentes em ambiente casual. No shopping, o restaurante é uma opção rápida e variada para quem busca comida oriental, com opções individuais e para compartilhar.",
                Location = "3 Andar - Praça de Alimentação - Loja 22",
                Phone = "(61) 3555-0456"
            }
        };
    }

    public static IEnumerable<Store> GetStoresByType(StoreType type) =>
        GetStores().Where(s => s.Type == type);

    public static IList<Movie> GetMovies()
    {
        return new List<Movie>
        {
            new Movie
            {
                Title = "Animais fantásticos",
                Subtitle = "O segredo de Dumbledore",
                Poster = "movie_dumbledore_poster.png",
                Banner = "movie_dumbledore_header.png",
                Duration = TimeSpan.FromMinutes(143), // 2h23
                Synopsis = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis. Class aptent taciti sociosqu ad litora torquent per conubia nostra..."
            },
            // NOVO FILME: À Procura da Felicidade
            new Movie
            {
                Title = "À Procura da Felicidade",
                Subtitle = "Drama • 1h57min • 12 anos",
                Poster = "movie_felicidade_poster.jpg",
                Banner = "movie_felicidade_header.jpg",
                Duration = TimeSpan.FromMinutes(117), // 1h57
                Synopsis = "Chris Gardner enfrenta dificuldades financeiras extremas enquanto cria sozinho o filho pequeno. Após conseguir uma vaga não remunerada em um programa de corretor da bolsa, ele luta para equilibrar trabalho, paternidade e sobrevivência, em uma jornada de resiliência e esperança."
            }
        };
    }
}
