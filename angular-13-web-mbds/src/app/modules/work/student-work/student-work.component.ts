import { Component, OnInit } from '@angular/core';
import { CourseTreeNode } from 'app/core/courses/courses.type';
import {MatTreeFlatDataSource, MatTreeFlattener} from '@angular/material/tree';
import { FlatTreeControl } from '@angular/cdk/tree';
import { ActivatedRoute, Router } from '@angular/router';

interface Node {
  expandable: boolean;
  level: number;
  id: number
  name: string;
  workName: string;
  grade: number;
  stateAssignment: string;
  stateWork: string;
  deliveryDate: string;
  submittedDate: string;
}

@Component({
  selector: 'app-student-work',
  templateUrl: './student-work.component.html',
  styleUrls: ['./student-work.component.scss']
})
export class StudentWorkComponent implements OnInit {

  private transformer = (node: CourseTreeNode, level: number): Node => {
    return {
      expandable: !!node.children && node.children.length > 0,
      level: level,
      id: node.id,
      name: node.name,
      stateAssignment: node.stateAssignment ?? '',
      workName: node.workName ?? '',
      stateWork: node.stateWork ?? '',
      grade: node.grade ?? 0,
      deliveryDate: node.deliveryDate?.toLocaleString() ?? '',
      submittedDate: node.submittedDate?.toLocaleString() ?? ''
    };
  }

  data: Array<CourseTreeNode> = []
  displayedColumns: string[] = ['name', 'stateAssignment', 'workName', 'stateWork', 'grade', 'action'];
  treeControl = new FlatTreeControl<Node>(node => node.level, node => node.expandable);
  treeFlattener = new MatTreeFlattener<CourseTreeNode, Node, Node>(this.transformer, node => node.level , node => node.expandable, node => node.children);
  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  constructor( private _route: ActivatedRoute, private _router: Router) { }

  ngOnInit(): void {
    this.data = this._route.snapshot.data.initialData;
    console.log(this.data)
    this.dataSource.data = this.data;
  }

  public details(element: Node){
    console.log(element)
    const root = element.level === 0 ? 'courses' : 'assignments';
    this._router.navigate(
      [`${root}/details/${element.id}`],
      { queryParams: { 'redirect': `/works/mywork`} }
    );
  }

}
