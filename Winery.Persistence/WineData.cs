using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineryStore.Contracts;

namespace WineryStore.Persistence
{
	public class InMemoryWineryDataStore : IWineryDataStore
	{
		public async Task<IEnumerable<Winery>> GetAllWineriesAsync()
		{
			return await Task.FromResult(WineryData);
		}

		public async Task<Winery> GetWineryByIdAsync(Guid id)
		{
			return await Task.FromResult(WineryData.SingleOrDefault(x => x.Id == id));
		}

		public async Task<IEnumerable<Wine>> GetAllWinesAsync()
		{
			return await Task.FromResult(WineryData.SelectMany(x => x.Wines));
		}

		public async Task<IEnumerable<Wine>> GetAllWinesFromWineryAsync(Guid wineryId)
		{
			return await Task.FromResult(
				WineryData
				.SingleOrDefault(x => x.Id == wineryId)
				.Wines);
		}

		public async Task<Wine> GetWineFromWineryByIdAsync(Guid wineryId, Guid wineId)
		{
			return await Task.FromResult(
				WineryData
				.SingleOrDefault(x => x.Id == wineryId)
				.Wines
				.SingleOrDefault(x => x.Id == wineId)
				);
		}
		
		public async Task<Winery> AddWineryAsync(Winery winery)
		{
			if (WineryData.Any(x => x.Name.Equals(winery.Name, StringComparison.InvariantCultureIgnoreCase)))
				throw new FormatException($"Winery with {winery.Name} allready exists");

			await Task.Run(() =>
			{
				winery.Id = Guid.NewGuid();
				foreach (var wine in winery.Wines)
					wine.Id = Guid.NewGuid();
				WineryData.Add(winery);
			});
			return winery;
		}

		public async Task<Winery> UpdateWineryAsync(Winery winery)
		{
			await Task.Run(() =>
			{
				var index = WineryData.FindIndex(x => x.Id == winery.Id);
				WineryData[index] = winery;
			});
			return await GetWineryByIdAsync(winery.Id);
		}

		public async Task<bool> RemoveWineryAsync(Guid WineryId)
		{
			await Task.Run(() =>
			{
				var index = WineryData.FindIndex(x => x.Id == WineryId);
				WineryData.RemoveAt(index);
			});
			return WineryData.Any(x => x.Id == WineryId);
		}

		static List<Winery> WineryData = new List<Winery>
		{
			new Winery
			{
				Id = new Guid("B3DC052C-C3A2-4607-9CA5-20E6DEDA3FC2"),
				Name = "Duckhorn",
				Region = "Napa",
				Country = "California",
				Wines = new List<Wine> {
					new Wine
					{
						Id = new Guid("9293677D-A36B-46CE-B2A0-9173858156EB"),
						Name = "Merlot Napa Valley Three Palms Vineyard",
						Color = WineColor.Red,
						Score = 95,
						Price = 98,
						Rank = 1,
						IssueDate = "Nov 30, 2017",
						Vintage = "2014",
						Note = "A powerful red, with concentrated flavors of red plum, cherry and boysenberry that are layered with plenty of rich spice and mineral accents. Touches of slate and cardamom make for a complex finish. Drink now through 2023. 3,170 cases made."
					},
					new Wine
					{
						Id = new Guid("961BD0D0-A0EB-43C8-B076-441E47FDC847"),
						Name = "Cabernet Sauvignon Napa Valley",
						Color = WineColor.Red,
						Score = 94,
						Price = 18,
						Rank = 32,
						IssueDate = "Jul 31, 1989",
						Vintage = "1986",
						Note = "On first sniff, this reveals itself as the complex wine it is, with smooth texture and deep plum, cherry, tar and vanilla flavors, long and perfumed. Drinkable now. 4,000 cases made."
					}
				}
			},
			new Winery
			{
				Id = new Guid("0A634FFD-874B-4796-8875-02181B0EBCC8"),
				Name = "Château Coutet",
				Region = "Bordeaux",
				Country = "France",
				Wines = new List<Wine> {
					new Wine
					{
						Id = new Guid("18CC07E8-712E-4830-BCEB-26F75F2AD3AB"),
						Name = "Barsac",
						Color = WineColor.Dessert,
						Score = 96,
						Price = 37,
						Rank = 3,
						IssueDate = "Mar 31, 2017",
						Vintage = "2014",
						Note = "This shows the vivid, racy side of Barsac, with streaming flavors of pineapple, yellow apple, green plum and white ginger, displaying lovely energy from start to finish. Ends with enough honeysuckle and orange blossom notes to balance the richness. Best from 2020 through 2035. 4,000 cases made."
					}
				}
			},
			new Winery
			{
				Id = new Guid("B39C7530-622D-456E-AFB6-760F3653F076"),
				Name = "Casanova di Neri",
				Region = "Tuscany",
				Country = "Italy",
				Wines = new List<Wine> {
					new Wine
					{
						Id = new Guid("05E45CF9-FEFC-408C-97A0-81B3598EF415"),
						Name = "Brunello di Montalcino",
						Color = WineColor.Red,
						Score = 95,
						Price = 65,
						Rank = 4,
						IssueDate = "Jun 15, 2017",
						Vintage = "2012",
						Note = "Effusive aromas and flavors of raspberry, cherry, floral, mineral and tobacco are at the center of this linear, vibrant red. Well-structured, this offers terrific length on the sinewy finish. Best from 2020 through 2035. 6,054 cases made."
					}
				}
			},
			new Winery
			{
				Id = new Guid("2E085A4D-A5D8-4352-8908-1B2103437885"),
				Name = "Bodegas Godeval",
				Region = "Spain",
				Country = "Spain",
				Wines = new List<Wine> {
					new Wine
					{
						Id = new Guid("9C2E678D-9AF2-48FA-A2BE-40B6238C5DCE"),
						Name = "Valdeorras ViÃ±a Godeval Cepas Vellas",
						Color = WineColor.White,
						Score = 92,
						Price = 20,
						Rank = 36,
						IssueDate = "Jun 15, 2015",
						Vintage = "2013",
						Note = "This alluring white delivers a broad range of flavors in a pillowy texture, while crisp, well-integrated acidity maintains the focus. Melon, coconut, spice and smoke flavors mingle harmoniously on the plush palate. The mineral element is fresh and long. Godello. Drink now through 2018. 1,800 cases imported."
					},
					new Wine
					{
						Id = new Guid("9C2E678D-9AF2-48FA-A2BE-40B6238C5DCE"),
						Name = "Valdeorras ViÃ±a Godeval Cepas Vellas",
						Color = WineColor.White,
						Score = 91,
						Price = 20,
						Rank = 75,
						IssueDate = "Sep 30, 2016",
						Vintage = "2014",
						Note = "Pear, peach and quince flavors mingle in this expressive white, while notes of mineral, tangerine and ginger add complexity. Shows depth and focus, with a clean, juicy finish. Drink now. 1,800 cases imported."
					}
				}
			},
			new Winery
			{
				Id = new Guid("D2835E85-89AB-4B23-A5D5-48DB93F778DD"),
				Name = "Sula",
				Region = "Nashik",
				Country = "India",
				Wines = new List<Wine> {
					new Wine
					{
						Id = new Guid("A05164DF-2BDB-4675-9AFA-9F5CBD9E69BD"),
						Name = "Rasa Shiraz",
						Color = WineColor.Red,
						Score = 92,
						Price = 20,
						Rank = 36,
						IssueDate = "Jun 15, 2018",
						Vintage = "2015",
						Note = "It is a complex wine, with power and finesse. Crafted from handpicked grapes from our own estate vineyards, Rasa Shiraz is aged for 12 months in premium French oak barrels and then further matured in the bottle before release."
					},
					new Wine
					{
						Id = new Guid("60E79B55-B2E8-45F7-9B89-6F9DE315D12E"),
						Name = "Dindori Reserve Viognier",
						Color = WineColor.White,
						Score = 91,
						Price = 20,
						Rank = 75,
						IssueDate = "Sep 30, 2017",
						Vintage = "2014",
						Note = "Dindori Reserve Viognier is an exotic elixir of peach and lychee flavours. Floral, spicy, stunning. Great as an aperitif and terrific with spicy food. Serve well chilled."
					},
					new Wine
					{
						Id = new Guid("8610C9AA-704E-4A10-99F3-086B980A8C4F"),
						Name = "Zinfandel Rosé",
						Color = WineColor.Rose,
						Score = 91,
						Price = 16,
						Rank = 100,
						IssueDate = "Sep 30, 2016",
						Vintage = "2015",
						Note = "Zinfandel Rose is ripe, fresh and fruity, with abundant aromas of cranberries and fresh strawberries. A versatile “anytime” wine great for picnics, parties and hot summer days. Lovely with poultry and spicy dishes. Serve well chilled."
					},
					new Wine
					{
						Id = new Guid("D9272018-DAE4-4F1A-BADF-B195C27BB057"),
						Name = "Brut",
						Color = WineColor.Sparkling,
						Score = 91,
						Price = 41,
						Rank = 12,
						IssueDate = "Sep 30, 2019",
						Vintage = "2017",
						Note = "A complex blend of Chenin Blanc, Chardonnay, Viognier, Pinot Noir, Riesling and Shiraz. This is one of the few “Méthode Champenoise” wines in the world to be crafted from six different grapes, resulting in something remarkable"
					},
					new Wine
					{
						Id = new Guid("9FB0680A-582C-4AA8-AE7F-21464B5BC5CA"),
						Name = "Late Harvest Chenin Blanc",
						Color = WineColor.Dessert,
						Score = 91,
						Price = 15,
						Rank = 5,
						IssueDate = "Sep 30, 2019",
						Vintage = "2019",
						Note = "Abounding with aromas of mango, honey and tropical fruit, our award winning Late Harvest Chenin Blanc is the perfect close to a delicious meal, but is also an elegant aperiti"
					}
				}
			}
		};
	}
}