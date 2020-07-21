import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import { Observable } from 'rxjs';
import {ITaskPut} from '../models/taskPut';
import {ITaskPost} from '../models/taskPost';
import {ITaskGet} from '../models/taskGet';

@Injectable({
  providedIn: 'root',
})
export class TaskService {

  constructor(private http: HttpClient) {}

  getAll(id: number, dateCreated?: Date) {
    let params = new HttpParams();
    params = params.append('projectId', id.toString());
    if (dateCreated) {
      params = params.append('dateCreated', this.formatDate(dateCreated).toString());
    }
    return this.http.get('/api/tasks/', { params: params } );
  }

  formatDate(date) {
    // tslint:disable-next-line:prefer-const
    let d = new Date(date),
      month = '' + (d.getMonth() + 1),
      day = '' + d.getDate(),
      // tslint:disable-next-line:prefer-const
      year = d.getFullYear();

    if (month.length < 2) {
      month = '0' + month;
    }
    if (day.length < 2) {
      day = '0' + day;
    }

    return [year, month, day].join('');
  }

  post(task: ITaskPost) {
    return this.http.post('/api/tasks/', task);
  }

  put(taskId: number, task: ITaskPut) {
    return this.http.put('/api/tasks/' + taskId, task);
  }
  delete(taskId: number) {
    return this.http.delete('/api/tasks/' + taskId);
  }

  start(taskId: number) {
    return this.http.post('/api/tasks/' + taskId + '/start', taskId);
  }

  cancel(taskId: number) {
    return this.http.put('/api/tasks/' + taskId + '/cancel', taskId);
  }

}
