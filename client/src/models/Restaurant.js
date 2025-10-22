import { APIItem } from "./APIItem.js"
import { Profile } from "./Profile.js"

export class Restaurant extends APIItem {
  constructor(data) {
    super(data)
    this.creatorId = data.creatorId
    this.description = data.description
    this.imgUrl = data.imgUrl
    this.isShutdown = data.isShutdown
    this.name = data.name
    this.reportCount = data.reportCount
    this.visits = data.visits
    this.owner = new Profile(data.owner)
  }
}

