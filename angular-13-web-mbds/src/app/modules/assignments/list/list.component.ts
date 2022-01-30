import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class AssignmentListComponent implements OnInit {

  page = 1;
  hideRequiredControl = new FormControl(false);
/*
  get page() { return this._state.page; }
  get pageSize() { return this._state.pageSize; }*/

  constructor() { }

  ngOnInit(): void {
  }

}
