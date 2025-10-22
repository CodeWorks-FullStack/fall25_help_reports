import { Profile } from "./Profile.js"

export class Account extends Profile {
  /**
   * @typedef AccountData
   * @property {string} id
   * @property {string} email
   * @property {string} name
   * @property {string} picture
   * 
   * @param {AccountData} data
   */
  constructor(data) {
    super(data)
    this.email = data.email
    // TODO add additional properties if needed
  }
}

