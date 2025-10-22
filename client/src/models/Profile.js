import { APIItem } from "./APIItem.js";


export class Profile extends APIItem {
  constructor(data) {
    super(data);
    this.name = data.name;
    this.picture = data.picture;
  }
}
