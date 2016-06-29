angular.module('shinetech-app').constant('mailtemplatecontent', {
    retrievepassword:{
        title: 'Reset password',
        mailtitle: 'Reset password',
        content: '<strong>Hello：</strong><br />'
        + '<p>You are trying to get reset your password, click <br /> {linkurl} </p> to reset your password.'
        + '<div style="text-align:right;">{webname} {weburl}</div>'
    },
    invitemail:{
        title: 'Invite',
        mailtitle: 'Invite',
        content: '<strong>Hello：</strong><br />'
        + '<p>You are invited to take participate in a project estimate, please click <br /> {linkurl} </p> to start.'
        + '<div style="text-align:right;">{webname} {weburl}</div>'
    }
});

