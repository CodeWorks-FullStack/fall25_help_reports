import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Restaurant } from "@/models/Restaurant.js"

class RestaurantsService {
  async updateRestaurant(restaurantId, restaurantData) {
    const response = await api.put(`api/restaurants/${restaurantId}`, restaurantData)
    logger.log('UPDATED RESTAURANT', response.data)
    AppState.restaurant = new Restaurant(response.data)
  }
  async deleteRestaurant(restaurantId) {
    const response = await api.delete(`api/restaurants/${restaurantId}`)
    logger.log('DELETED RESTAURANT', response.data)
  }
  async getRestaurantById(restaurantId) {
    AppState.restaurant = null
    const response = await api.get(`api/restaurants/${restaurantId}`)
    logger.log('GOT RESTAURANT 🏩', response.data)
    AppState.restaurant = new Restaurant(response.data)
  }
  async getRestaurants() {
    const response = await api.get('api/restaurants')
    logger.log('GOT RESTAURANTS 🏪🏪🏪🏪', response.data)
    AppState.restaurants = response.data.map(pojo => new Restaurant(pojo))
  }
}
export const restaurantsService = new RestaurantsService()