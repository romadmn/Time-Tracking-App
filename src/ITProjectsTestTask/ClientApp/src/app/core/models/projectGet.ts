import {ITaskGet} from './taskGet';

export interface IProjectGet {
  id?: number;
  name: string;
  tasks: ITaskGet[];
  totalTimeSpentOnTasks?: Date;
}
