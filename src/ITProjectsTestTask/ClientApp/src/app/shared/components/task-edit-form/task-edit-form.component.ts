import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {ITaskGet} from '../../../core/models/taskGet';
import {ITaskPut} from '../../../core/models/taskPut';
import {TaskService} from '../../../core/services/task.service';

@Component({
  selector: 'app-task-edit-form',
  templateUrl: './task-edit-form.component.html',
  styleUrls: ['./task-edit-form.component.css']
})
export class TaskEditFormComponent implements OnInit {
  @Output() onCancel: EventEmitter<void> = new EventEmitter<void>();
  @Input() task: ITaskGet;
  @Input() projectId: number;
  editTaskForm: FormGroup;

  constructor(private taskService: TaskService ) { }

  ngOnInit(): void {
    this.editTaskForm = new FormGroup({
      ticket: new FormControl(this.task.ticket, [Validators.required, Validators.maxLength(50)]),
      description: new FormControl(this.task.description, [Validators.required, Validators.maxLength(1000)]),
    });
  }

  onSubmit() {
    let task: ITaskPut = {
      id: this.task.id,
      ticket: this.editTaskForm.get('ticket').value,
      description: this.editTaskForm.get('description').value,
      projectId: this.projectId,
      createDate: this.task.createDate
    };
    this.taskService.put(task.id, task).subscribe(
      () => {
        this.onCancel.emit();
      });
  }

  cancel() {
    this.onCancel.emit();
  }
}
