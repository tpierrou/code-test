using Api.Models;
using Api.Utilities;

namespace Api.Domain
{
    public class ItemsService : IItemsService
    {
        private readonly IApiClient apiClient;

        public ItemsService(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IEnumerable<ItemDto>> GetItems()
        {
            var response = await apiClient.GetAsync("fetch");
            var itemDtos = await response.Content.ReadFromJsonAsync<IEnumerable<ItemDto>>();

            return itemDtos ?? throw new HttpRequestException("No items found");
        }

        public async Task<IEnumerable<ItemDto>> GetItems(string[] itemIds)
        {
            var response = await apiClient.GetAsync("fetch");
            var itemDtos = await response.Content.ReadFromJsonAsync<IEnumerable<ItemDto>>();

            var filteredItemDtos = FilterItemDtos(itemDtos, itemIds);

            return filteredItemDtos ?? throw new HttpRequestException("No items found");
        }

        private IEnumerable<ItemDto> FilterItemDtos(IEnumerable<ItemDto>? itemDtos, string[] itemIds)
        {
            if(itemDtos == null)
            {
                throw new ArgumentNullException();
            }

            var itemIdsSet = new HashSet<string>(itemIds);

            return itemDtos
                .Where(item => itemIdsSet.Contains(item.Id));
        }
    }
}