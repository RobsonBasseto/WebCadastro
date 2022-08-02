const usuario={template:`
<div>

        <button type="button" class="btn btn-primary m-2 fload-end" data-bs-toggle="modal"
                data-bs-target="#exampleModal" @click="addClick()">Adicionar</button>

        <table class="table">
            <thead class="thead-dark">
                <tr>
                    <th> ID usuario</th>
                    <th>Nome Usuario</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="u in usuarios">
                    <td>{{u.idusuario}}</td>
                    <td>{{u.nomeusuario}}</td>
                    <td>
                        <button type="button" class="btn btn-light mr-1" data-bs-toggle="modal"
                                data-bs-target="#exampleModal" @click="editClick(u)">editar</button>
                        <button type="button" class="btn btn-light mr-1" @click="deleteClick(u.idusuario)">deletar</button>
                    </td>
                </tr>
            </tbody>
        </table>

        <div class="modal fade" id="exampleModal" tabindex="-1">
            <div class="modal-dialog modal-lg modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalLabel">{{modalTitle}}</h5>
                        <button type="button" class="bnt-close" data-bs-dismiss="modal" aria-label="Close">X
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="input-group mb-3">
                            <span class="input-group-text">Nome do usuario</span>
                            <input type="text" class="form-control" v-model="nomeusuario">
                        </div>
                        <button type="button" @click="createClick()" v-if="idusuario==0" class="btn btn-primary">Salvar</button>
                        <button type="button" @click="updateClick()"  v-if="idusuario!=0" class="btn btn-primary">Alterar</button>

                    </div>
                </div>
            </div>
        </div>

    </div>
`,
data(){
    return{
        usuarios:[],
        modalTitle:"",
        idusuario:0,
        nomeusuario:""
    }
},
methods:{
    refreshData(){
        axios.get(variaveis.API_URL+"usuario").then((response)=>{
            this.usuarios=response.data;
        });
    },
    addClick(){
        this.modalTitle="Adicionar usuario";
        this.idusuario=0;
        this.nomeusuario="";
    },
    editClick(u){
        this.modalTitle="Editar usuario";
        this.idusuario= u.idusuario;
        this.nomeusuario= u.nomeusuario;
    },
    createClick(){
        axios.post(variaveis.API_URL+"usuario",{
            nomeusuario:this.nomeusuario
        }).then((response)=>{
            this.refreshData();
            alert(response.data);
        });
    },
    updateClick(){
        axios.put(variaveis.API_URL+"usuario",{
            idusuario:this.idusuario,
            nomeusuario:this.nomeusuario
        }).then((response)=>{
            this.refreshData();
            alert(response.data);
        });
    },
    deleteClick(id){
        if(!confirm("Tem certeza que deseja deletar?")){
            return;
        }
        axios.delete(variaveis.API_URL+"usuario/"+id                
        ).then((response)=>{
            this.refreshData();
            alert(response.data);
        });
    }
},
mounted:function(){
    this.refreshData();
}
}