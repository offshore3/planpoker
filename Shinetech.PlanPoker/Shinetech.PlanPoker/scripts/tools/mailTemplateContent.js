angular.module('shinetech-app').constant('mailtemplatecontent', {
    retrievepassword:{
        title: '用户取回密码',
        mailtitle: '找回密码提示(请勿回复此邮件)',
        content: '<strong>您好：</strong><br />'
        + '<p>您在<span style="color:#FF0000;">{webname}</span>点击了“忘记密码”找回申请，故系统自动为您发送了这封邮件。您可以点击以下链接修改您的密码：<br /> {linkurl} </p><hr />'
        + '<p>如果您不需要修改密码，或者您从未点击过“忘记密码”按钮，请忽略本邮件。<br />任何疑问，请拨打客服热线咨询：{webtel}。谢谢您的支持！</p>'
        + '<div style="text-align:right;">{webname} {weburl}</div>'
    },
    invitemail:{
        title: '邀请评估',
        mailtitle: '邀请参加评估提示(请勿回复此邮件)',
        content: '<strong>您好：</strong><br />'
        + '<p>您的好友在<span style="color:#FF0000;">{webname}</span>点击了“邀请评估”请求您参见评估项目，故系统自动为您发送了这封邮件。您可以点击以下链接参加评估：<br /> {linkurl} </p><hr />'
        + '<p>如果您不愿意接受，请忽略本邮件。<br />任何疑问，请拨打客服热线咨询：{webtel}。谢谢您的支持！</p>'
        + '<div style="text-align:right;">{webname} {weburl}</div>'
    }
});

