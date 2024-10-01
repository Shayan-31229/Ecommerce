$(function(){
    $('.skillLists').sortable({
        update:function(e,i){
            var ordr = {};
            $(i.item.parent('ul').find('li')).each(function(){
                ordr[$(this).find('a').attr('rel')]=$(this).index();
            });
            $.ajax({
                url:domain+'/admin/skills/reorder',
                type:'post',
                data:ordr,
                headers: {
                    'X-CSRF-Token': csrfToken
                },
                success:function(data){
                    //alert(data);
                    toastr.success(data);
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseText);
                    toastr.error(XMLHttpRequest.responseText);
                }
            });
        },
    });
    
    $("#modalBtnSave").click(function(){
        var skillData={};
        if($(".modal-title").html()=='Update Skill' && $("#myModal #txtId").val()!=''){
            skillData['id'] = $("#myModal #txtId").val();
        }
        skillData['skill'] = $("#myModal #txtSkill").val();
        skillData['percent'] = $("#myModal #txtPercent").val();
        skillData['from_year'] = $("#myModal #txtFrom").val();
        skillData['to_year'] = $("#myModal #txtTo").val();
        skillData['status'] = $("#myModal #txtStatus").val();
        $.ajax({
            url:domain+'/admin/skills/update',
            type:'post',
            data:skillData,
            headers: {
                'X-CSRF-Token': csrfToken
            },
            success:function(data){
                if(data==1){
                    window.location.replace(domain+'/admin/skills/percent');
                }else{
                    //alert(data);
                    toastr.error(data);
                }
            },
            error: function(XMLHttpRequest, textStatus, errorThrown) {
                //alert(XMLHttpRequest.responseText);
                toastr.error(XMLHttpRequest.responseText);
            }
        });
    });
    $("#btnAddNewSkill").click(function(e){
        e.preventDefault();
        $("#myModal").modal({
            show:true
        });
        $("#myModal .modal-title").html('Add New Skill');
        $("#myModal #txtId").val('');
        e.preventDefault();
    });
    $(".txtRow").change(function(){
        rowChanged = true;
    });
    $(".txtRow").mouseleave(function(){
        if(rowChanged==true){
            var skill_id = $(this).attr('data');
            var skill_row = $(this).val();
            $.ajax({
                url:domain+'/admin/skills/update_row',
                type:'post',
                data:{id:skill_id,row:skill_row},
                headers: {
                    'X-CSRF-Token': csrfToken
                },
                success:function(data){
                    if(data==1){
                        //alert('row updated successfully.');
                        toastr.success('row updated successfully.');
                    }else{
                        //alert(data);
                        toastr.error(data);
                    }
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseText);
                    toastr.error(XMLHttpRequest.responseText);
                }
            });
            rowChanged = false;
        }
    });
    $("a.skills").click(function(e){
        var skillData = jQuery.parseJSON($(this).next('.skillData').html());
        $("#myModal").modal({
            show:true
        });
        $("#myModal .modal-title").html('Update Skill');
        $("#myModal #txtId").val(skillData.id);
        $("#myModal #txtSkill").val(skillData.skill);
        $("#myModal #txtPercent").val(skillData.percent);
        $("#myModal #txtFrom").val(skillData.from_year);
        $("#myModal #txtTo").val(skillData.to_year);
        $("#myModal #txtStatus").val(skillData.status);
        e.preventDefault();
    });
    $('#myModal').on('hidden.bs.modal', function () {
        $("#myModal input,#myModal select").val('');
    });
});