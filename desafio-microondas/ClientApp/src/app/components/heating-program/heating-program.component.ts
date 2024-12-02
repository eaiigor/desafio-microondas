import {Component, EventEmitter, Input, Output} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {AddItemDialogComponent} from "../add-item-dialog/add-item-dialog.component";
import {HeatingProgramService} from "../../services/heating-program/heating-program.service";
import {Observable} from "rxjs";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-heating-program',
  templateUrl: './heating-program.component.html',
  styleUrls: ['./heating-program.component.scss']
})
export class HeatingProgramComponent {

  items$: Observable<any[]>
  @Output() cardSelected = new EventEmitter<any>();


  constructor(private dialog: MatDialog, private heatingProgramService: HeatingProgramService,
              private toastrService: ToastrService
  ) {
    this.items$ = heatingProgramService.getHeatingPrograms();
  }

  startHeating(card: any) {
    this.cardSelected.emit(card);
  }


  delete(card: any) {
    this.heatingProgramService.deleteHeatingProgram(card.id).subscribe(() => {
      this.items$ = this.heatingProgramService.getHeatingPrograms();
      this.toastrService.success('Programa de aquecimento deletado com sucesso');
    });
  }

  create() {
    const dialogRef = this.dialog.open(AddItemDialogComponent, {
      width: '300px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.items$ = this.heatingProgramService.getHeatingPrograms();
      }
    });
  }

}
