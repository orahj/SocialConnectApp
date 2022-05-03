import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NotFoundComponent } from "./errors/not-found/not-found.component";
import { ServerErrorComponent } from "./errors/server-error/server-error.component";
import { TestErrorsComponent } from "./errors/test-errors/test-errors.component";
import { HomeComponent } from "./home/home.component";
import { ListComponent } from "./list/list.component";
import { MemberDetailsComponent } from "./members/member-details/member-details.component";
import { MemberEditComponent } from "./members/member-edit/member-edit.component";
import { MemberListComponent } from "./members/member-list/member-list.component";
import { MessagesComponent } from "./messages/messages.component";
import { AuthGuard } from "./_guards/auth.guard";
import { PreventUnsavedChangesGuard } from "./_guards/prevent-unsaved-changes.guard";

const routes: Routes = [
    {path:'', component: HomeComponent},
    {
        path:'',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path:'members', component: MemberListComponent},
            {path:'members/:username', component: MemberDetailsComponent},
            {path:'member/:edit', component: MemberEditComponent, canDeactivate:[PreventUnsavedChangesGuard]},
            {path:'list', component: ListComponent},
            {path:'messages', component: MessagesComponent},
        ]
    },
    {path:'not-found', component:NotFoundComponent},
    {path:'server-error', component:ServerErrorComponent},
    {path:'errors', component: TestErrorsComponent},
    {path:'**', component: NotFoundComponent,pathMatch:'full'}
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule{}