export interface ITaskPut {
  id?: number;
  ticket: string;
  description: string;
  projectId: number;
  createDate: Date;
}
