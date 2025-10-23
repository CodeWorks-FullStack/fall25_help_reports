import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Report } from "@/models/Report.js"


class ReportsService {
  async getReportsForRestaurant(restaurantId) {
    const response = await api.get(`api/restaurants/${restaurantId}/reports`)
    logger.log('📄📄📄', response.data)
    const reports = response.data.map(mojo => new Report(mojo))
    AppState.reports = reports
  }
  async createReport(reportData) {
    const response = await api.post('api/reports', reportData)
    logger.log('📕', response.data)


    // update the details page
    // Only update the appstate if you are at the right spot in the app to do so
    if (reportData.restaurantId == AppState.restaurant?.id) {
      AppState.reports.push(new Report(response.data))
      // window.location.reload()👈 BAD PAUL, NO!
      AppState.restaurant.reportCount += 1
    }

    // update the home view
    const restaurantReported = AppState.restaurants.find(rest => rest.id == reportData.restaurantId)
    if (!restaurantReported) { return }
    restaurantReported.reportCount += 1


  }
}

export const reportsService = new ReportsService()