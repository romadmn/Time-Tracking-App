import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {IProjectGet} from '../../../core/models/projectGet';
import {ProjectService} from '../../../core/services/project.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {IProjectPut} from '../../../core/models/projectPut';

@Component({
  selector: 'app-project-edit-form',
  templateUrl: './project-edit-form.component.html',
  styleUrls: ['./project-edit-form.component.css']
})
export class ProjectEditFormComponent implements OnInit {
  @Output() onCancel: EventEmitter<void> = new EventEmitter<void>();
  @Input() project: IProjectGet;
  editProjectForm: FormGroup;

  constructor(private projectService: ProjectService ) { }

  ngOnInit(): void {
    this.editProjectForm = new FormGroup({
      name: new FormControl(this.project.name, [Validators.required, Validators.maxLength(50)])
    });
  }

  onSubmit() {
    let project: IProjectPut = {
      id: this.project.id,
      name: this.editProjectForm.get('name').value
    };
      this.projectService.put(project.id, project).subscribe(
        () => {
          this.onCancel.emit();
        });
  }

  cancel() {
    this.onCancel.emit();
  }
}
