import { Profile } from "./Profile.js"

export class Report {
  constructor(data) {
    this.id = data.id
    this.title = data.title
    this.body = data.body
    this.creatorId = data.creatorId
    this.creator = new Profile(data.creator)
    this.score = data.score
    this.createdAt = new Date(data.createdAt)
    // and the list goes on...
  }
}