export interface ITaskGet {
  id?: number;
  ticket: string;
  description: string;
  startDate: Date;
  createDate: Date;
  cancelDate: Date;
  timeSpentOnTheTask: Date;
}
