import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {IProjectGet} from '../models/projectGet';
import {IProjectPost} from '../models/projectPost';
import {IProjectPut} from '../models/projectPut';

@Injectable({
  providedIn: 'root',
})
export class ProjectService {

  constructor(private http: HttpClient) {}

  getAll(): Observable<IProjectGet[]> {
    return this.http.get<IProjectGet[]>('/api/projects/');
  }

  getById(id: number): Observable<IProjectGet> {
    return this.http.get<IProjectGet>('/api/projects/' + id);
  }

  post(project: IProjectPost) {
    return this.http.post('/api/projects/', project);
  }

  put(projectId: number, project: IProjectPut) {
    return this.http.put('/api/projects/' + projectId, project);
  }
  delete(projectId: number) {
    return this.http.delete('/api/projects/' + projectId);
  }

}
