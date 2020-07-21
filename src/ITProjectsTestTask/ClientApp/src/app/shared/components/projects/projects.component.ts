import {Component, ComponentFactoryResolver, OnInit, ViewChild} from '@angular/core';
import {IProjectGet} from '../../../core/models/projectGet';
import {ProjectService} from '../../../core/services/project.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {IProjectPost} from '../../../core/models/projectPost';
import {RefDirective} from '../../directives/ref.directive';
import {ProjectEditFormComponent} from '../project-edit-form/project-edit-form.component';
import {ITaskGet} from '../../../core/models/taskGet';
import {TaskEditFormComponent} from '../task-edit-form/task-edit-form.component';
import {MatPaginator, MatSort, MatTableDataSource} from '@angular/material';
import {TaskService} from '../../../core/services/task.service';
import {IProjectPut} from '../../../core/models/projectPut';
import {ITaskPost} from '../../../core/models/taskPost';

@Component({
  selector: 'app-announcements',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {
  @ViewChild(RefDirective, {static: false}) refDir: RefDirective;
  @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: false}) sort: MatSort;

  hideAddProjectForm: boolean = true;
  hideAddTaskForm: boolean = true;
  projects: IProjectGet[];
  displayedColumns = ['id', 'times', 'ticket', 'description', 'start', 'end', 'editAndDelete'];
  project: IProjectGet;
  tasks: MatTableDataSource<ITaskGet>;
  addProjectForm: FormGroup = new FormGroup({
    id: new FormControl(0),
    name: new FormControl('', [Validators.required, Validators.maxLength(50)])
  });
  addTaskForm: FormGroup = new FormGroup({
    id: new FormControl(0),
    ticket: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    description: new FormControl('', [Validators.required, Validators.maxLength(1000)])
  });

  constructor(private projectService: ProjectService,
              private taskService: TaskService,
              private resolver: ComponentFactoryResolver) { }

  ngOnInit(): void {
    this.getAllProjects();
  }
  getAllProjects() {
      this.projectService.getAll().subscribe((value: IProjectGet[]) => {
        this.projects = value;
      });
  }
  getProjectWithTasks(id: number) {
    this.projectService.getById(id).subscribe((value: IProjectGet) => {
      this.project = value;
      this.tasks = new MatTableDataSource<ITaskGet>(this.project.tasks)
    });
  }


  getTasksForDate(date: any) {
    this.taskService.getAll(this.project.id, date).subscribe((value: ITaskGet[]) => {
      this.tasks = new MatTableDataSource<ITaskGet>(value)
    });
  }

  onSubmit() {
    const newProject: IProjectPost = {
      id: 0,
      name: this.addProjectForm.get('name').value
    };
    this.projectService.post(newProject).subscribe(() => {
      this.ngOnInit();
      this.addProjectForm.reset();
    });
  }
  showEditProjectForm(project: IProjectGet) {
    const formFactory = this.resolver.resolveComponentFactory(ProjectEditFormComponent);
    const instance = this.refDir.containerRef.createComponent(formFactory).instance;
    instance.project = project
    instance.onCancel.subscribe(() => {this.refDir.containerRef.clear(); this.getAllProjects(); });
  }
  showEditTaskForm(task: ITaskGet) {
    const formFactory = this.resolver.resolveComponentFactory(TaskEditFormComponent);
    const instance = this.refDir.containerRef.createComponent(formFactory).instance;
    instance.task = task
    instance.projectId = this.project.id;
    instance.onCancel.subscribe(() => {this.refDir.containerRef.clear(); this.getProjectWithTasks(this.project.id) });
  }
  deleteProject(id: number) {
    this.projectService.delete(id).subscribe(() => {
      this.getAllProjects();
    });
  }

  deleteTask(id: number) {
    this.taskService.delete(id).subscribe(() => {
      this.getProjectWithTasks(this.project.id)
    });
  }
  startTask(id: number) {
    this.taskService.start(id).subscribe(() => {
      this.getProjectWithTasks(this.project.id)
    });
  }
  cancelTask(id: number) {
    this.taskService.cancel(id).subscribe(() => {
      this.getProjectWithTasks(this.project.id)
    });
  }
  ngAfterViewInit() {
    this.tasks.paginator = this.paginator;
    this.tasks.sort = this.sort;
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.tasks.filter = filterValue;
  }

  onAddProject() {
    let project: IProjectPost = {
      name: this.addProjectForm.get('name').value
    };
    this.projectService.post(project).subscribe(
      () => {
        this.getAllProjects();
        this.hideAddProjectForm = true;
      });
  }

  onAddTask() {
    let task: ITaskPost = {
      ticket: this.addTaskForm.get('ticket').value,
      description: this.addTaskForm.get('description').value,
      projectId: this.project.id
    };
    this.taskService.post(task).subscribe(
      () => {
        this.getProjectWithTasks(this.project.id);
        this.hideAddTaskForm = true;
      });
  }
}
