﻿import {Component, OnInit, AfterViewInit, OnDestroy} from 'angular2/core'
import {Router} from 'angular2/router'
import * as services from '../services/services'
import * as model from '../dal/models'
@Component({ template: require('./calendar.editor.html!text') })
export class CalendarEditorComponent implements AfterViewInit, OnDestroy {
    item: model.CalendarItem;
    HideProblem: boolean = true;
    HideSuccess: boolean = true;
    pending: boolean = false;
    constructor(private dataService: services.DataService, private router: Router) {
        this.item = { DataDate: null, DataDateString: '', ID: -1, Text_Eng: '', Text_Heb: '', TimeStamp: null, ToDelete: false, Visible: true };
    }
    ngAfterViewInit() {
        CKEDITOR.replace('editor_heb');
        CKEDITOR.replace('editor_eng');
    }

    ngOnDestroy() {
        for (name in CKEDITOR.instances) {
            CKEDITOR.instances[name].destroy(true);
        }
    }

    updateCalendar() {
        var eng = CKEDITOR.instances['editor_eng'].getData();
        var heb = CKEDITOR.instances['editor_heb'].getData();
        var calendarItems: model.CalendarItem[] = [{ DataDate: new Date(this.item.DataDateString), Text_Eng: eng, ID: -1, Text_Heb: heb, ToDelete: this.item.ToDelete, TimeStamp: new Date(), Visible: this.item.Visible, DataDateString: '' }];

        var req: model.UpdateCalendarRequest = { CalendarItems: calendarItems };
        this.dataService.ConnectToApiData(req, 'api/Data/UpdateCalendarItems').subscribe((res: model.UpdateResponse) => this.postUpdate(res.UpdateStatus), (err: model.DataError) => this.postUpdateFail());
    }

    postUpdate(updateStatus: model.UpdateStatus): void {
        switch (updateStatus) {
            case model.UpdateStatus.Fail:
                this.postUpdateFail();
                break;
            case model.UpdateStatus.Success:
                this.postUpdateSuccess();
                break;
        }
    }

    postUpdateFail() {
        console.log('error in LinkComponent in updateMenuItems');
        this.updateVariables(false, false, true);
    }
    postUpdateSuccess() {
        this.updateVariables(false, true, false);
        this.toHome();

    }

    updateVariables(pending: boolean = this.pending, hideProblem: boolean = this.HideProblem, hideSuccess: boolean = this.HideSuccess): void {
        this.pending = pending;
        this.HideProblem = hideProblem;
        this.HideSuccess = hideSuccess;

    }

    toHome() {
        this.router.navigate(['Home']);
    }

}