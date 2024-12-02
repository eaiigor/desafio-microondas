import {Component} from '@angular/core';
import {MatDialogRef} from '@angular/material/dialog';
import {HeatingProgramService} from "../../services/heating-program/heating-program.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-add-item-dialog',
  templateUrl: './add-item-dialog.component.html',
})
export class AddItemDialogComponent {
  name: string = '';
  time: string = '';
  power: string = '';

  constructor(
    public dialogRef: MatDialogRef<AddItemDialogComponent>,
    private heatingProgramService: HeatingProgramService,
    private toastrService: ToastrService
  ) {
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onAdd(): void {


    const minutes = +this.time.split(':')[0];
    const seconds = +this.time.split(':')[1];

    const time = minutes * 60 + seconds;
    this.heatingProgramService.createHeatingProgram(time, +this.power, this.name).subscribe((response) => {
        this.dialogRef.close(response);
        this.toastrService.success('Programa de aquecimento criado com sucesso');
      },
      () => {
        this.toastrService.error('Erro ao criar programa de aquecimento');
      });
  }

}
