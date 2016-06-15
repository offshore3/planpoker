angular.module('shinetech-app').constant('mailtemplatecontent', {
    title: '用户取回密码',
    mail_title: '找回密码提示(请勿回复此邮件)',
    content: '<strong>{username}，您好：</strong><br />'
    +'<p>您在<span style="color:#FF0000;">{webname}</span>点击了“忘记密码”找回申请，故系统自动为您发送了这封邮件。您可以点击以下链接修改您的密码：<br />'
    +'<a href="{linkurl}" target="_blank">{linkurl}</a></p>'
    +'<hr />'
    +'<p>如果您不需要修改密码，或者您从未点击过“忘记密码”按钮，请忽略本邮件。<br />'
    +'任何疑问，请拨打客服热线咨询：{webtel}。谢谢您的支持！</p>'
    +'<div style="text-align:right;">{webname} {weburl}</div>'
});

