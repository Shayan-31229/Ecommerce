$(function(){
    var myOptions = {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        },
        responsive: true,
    };
    var ctx1 = document.getElementById('chartRegistrations').getContext('2d');
    var regChart = new Chart(ctx1, {
        type: 'bar', 
        options: myOptions
    });

    var ctx2 = document.getElementById('ChartAttendance').getContext('2d');
    var attChart = new Chart(ctx2, {
        type: 'bar', 
        options: myOptions
    });

    
    var ctx3 = document.getElementById('ChartRenewal').getContext('2d');
    var renewChart = new Chart(ctx3, {
        type: 'bar', 
        options: myOptions
    });

    
    var ctx4 = document.getElementById('ChartReissue').getContext('2d');
    var reissueChart = new Chart(ctx4, {
        type: 'bar', 
        options: myOptions
    });
    
    $('.card-reg .txtRegistrations,.card-reg .reg_report_type').change(function(){  
        updateRegChart(regChart);
    });

    $('.regChartType').click(function(e){
        e.preventDefault();
        $('.regChartType.active').removeClass('active');
        $(this).addClass('active');
        // regChart.config.type =$(this).text(); 
        // regChart.attChart.update();
        updateRegChart(regChart);
         
    });

    updateRegChart(regChart);

 /************* Attendance**/
    $('.card-att .txtAttendance,.card-att .att_report_type').change(function(){  
        updateAttChart(attChart);
    });

    $('.attChartType').click(function(e){
        e.preventDefault();
        $('.attChartType.active').removeClass('active');
        $(this).addClass('active');
        // attChart.config.type =$(this).text();
        // attChart.update();
        updateAttChart(attChart); 
    });

    updateAttChart(attChart);

    /*************Renewal**/
    $('.card-renew .txtRenewals,.card-renew .Renew_report_type').change(function(){  
        updateRewalChart(renewChart);
    });

    $('.RenewChartType').click(function(e){
        e.preventDefault();
        $('.RenewChartType.active').removeClass('active');
        $(this).addClass('active');
        // attChart.config.type =$(this).text();
        // attChart.update();
        updateRewalChart(renewChart); 
    });

    updateRewalChart(renewChart);

    /*************ReIssue**/
    $('.card-reissue .txtReissue,.card-reissue .Reissue_report_type').change(function(){  
        updateReIssueChart(reissueChart);
    });

    $('.ReissueChartType').click(function(e){
        e.preventDefault();
        $('.ReissueChartType.active').removeClass('active');
        $(this).addClass('active');
        // attChart.config.type =$(this).text();
        // attChart.update();
        updateReIssueChart(reissueChart); 
    });

    updateReIssueChart(reissueChart);
});


function updateRegChart(chart){
    var start_date = $('.card-reg .txtRegistrations.start_date').val();
    var end_date = $('.card-reg .txtRegistrations.end_date').val();
    var report_type = $('.card-reg .reg_report_type:checked').val();
    $.ajax({
    url:domain+'/en/users/get_registration_chart_data',
    type:'get',
    data:{'start_date':start_date,'end_date':end_date,'report_type':report_type},
    beforeSend:function(){

    },
    success:function(resp){
        chart.data = JSON.parse(resp); 
        chart.config.type = $('.regChartType.active').text();
        chart.update();
    },error:function(){

    }
    });
}

function updateAttChart(chart){
    var start_date = $('.card-att .txtAttendance.start_date').val();
    var end_date = $('.card-att .txtAttendance.end_date').val();
    var report_type = $('.card-att .att_report_type:checked').val();
    $.ajax({
    url:domain+'/en/users/get_attendance_chart_data',
    type:'get',
    data:{'start_date':start_date,'end_date':end_date,'report_type':report_type},
    beforeSend:function(){

    },
    success:function(resp){
        chart.data = JSON.parse(resp);  
        chart.config.type = $('.attChartType.active').text();
        chart.update();
    },error:function(){

    }
    });
}

function updateRewalChart(chart){
    var start_date = $('.card-renew .txtRenewals.start_date').val();
    var end_date = $('.card-renew .txtRenewals.end_date').val();
    var report_type = $('.card-renew .Renew_report_type:checked').val();
    $.ajax({
    url:domain+'/en/users/get_renewal_chart_data',
    type:'get',
    data:{'start_date':start_date,'end_date':end_date,'report_type':report_type},
    beforeSend:function(){

    },
    success:function(resp){
        chart.data = JSON.parse(resp);  
        chart.config.type = $('.RenewChartType.active').text();
        chart.update();
    },error:function(){

    }
    });
}


function updateReIssueChart(chart){
    var start_date = $('.card-reissue .txtReissue.start_date').val();
    var end_date = $('.card-reissue .txtReissue.end_date').val();
    var report_type = $('.card-reissue .Reissue_report_type:checked').val();
    $.ajax({
    url:domain+'/en/users/get_reissue_chart_data',
    type:'get',
    data:{'start_date':start_date,'end_date':end_date,'report_type':report_type},
    beforeSend:function(){

    },
    success:function(resp){
        chart.data = JSON.parse(resp);  
        chart.config.type = $('.ReissueChartType.active').text();
        chart.update();
    },error:function(){

    }
    });
}