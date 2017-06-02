import os, re, json, sys

def audio_checker():
    print('\nchecking audio\n')
    stdout = sys.stdout
    #sys.stdout = open('audiolog.txt', 'w')
    json_files = [f for f in os.listdir() if f.endswith('.json') and re.search(r'^[0-9]\.[0-9]', f)]
    for jsonfile in json_files:
        day = re.search(r'^[0-9]', jsonfile).group(0) #day = jsonfile[:1]
        patron = re.search(r'(Boss)|(Businessman)|(Artist)|(Server)|(Adultress)|(Tourist)', jsonfile).group(0)
        audio_file_path = '../../Resources/Dialogue/' + patron + '/Day' + day
        audio_files = [f[:-4] for f in os.listdir(audio_file_path) if f.endswith('.wav')]
        
        with open(jsonfile) as data_file:
            data = json.load(data_file)
        
        nodes = data['nodes']
        node_audios = []
        for node in nodes:
            node_audios.extend(node['dialogue'])
        
        node_audios = list(set(node_audios))
        
        flag = False
        for audio in node_audios:
            if audio not in audio_files:
                print(jsonfile, "MISSING AUDIO FILE", audio)
                flag = True
        
        if not flag:
            print(jsonfile, 'PASSED')
        else:
            print(jsonfile, 'FAILED')
    sys.stdout = stdout

def node_checker():
    print('\nchecking node\n')
    files = [f for f in os.listdir() if f.endswith('.json') and re.search(r'^[0-9]\.[0-9]', f)]
    for file in files:
        try:
            #opening json file
            with open(file) as data_file:
                data = json.load(data_file)
            
            #checking main fields
            if 'agentAttr' in data:
                attributes = data['agentAttr']
            else:
                raise ValueError(file, 'no \'agentAttr\'')
            if 'nodes' in data:
                nodes = data['nodes']
            else:
                raise ValueError(file, 'no \'nodes\'')
            
            #checking attribute values
            for attribute in attributes:
                if attribute not in ['goal', 'patience', 'mood']:
                    raise ValueError(file, 'unknown attribute', attribute)
            for attribute in ['goal', 'patience', 'mood']:
                if attribute not in attributes:
                    raise KeyError(file, 'missing attribute', attribute)
            
            #checking possible nodes
            node_names = [n['name'] for n in nodes]
            node_goto = []
            node_flag = False
            for node in nodes:
                if 'toNode' in node:
                    node_goto.extend(node['toNode'])
                if node['name'] == 'End':
                    pass
                elif 'noResponse' not in node:
                    print(file, node['name'], 'does not have \'noResponse\'')
                else:
                    node_goto.extend([node['noResponse']])
            
            node_goto = list(set(node_goto))
            
            for node in node_goto:
                if node not in node_names:
                    print(file, 'missing node', node)
                    node_flag = True
            '''
            for node in node_names:
                if node not in node_goto and node not in ['Start', 'End']:
                    print(file, 'cannot reach', node)
                    node_flag = True
            '''
            if node_flag:
                pass
            else:
                print(file, 'PASSED')
        except json.JSONDecodeError:
            print('Error Decoding ' + file + '. Check syntax.')
        except ValueError as error:
            print(error.args)
        except KeyError as error:
            print(error.args)
        

if __name__ == '__main__':
    commands = ['node', 'audio']
    if len(sys.argv) < 2:
        print('USAGE: program.py node|audio')
    for x in range(1, len(sys.argv)):
        if sys.argv[x] in commands:
            if sys.argv[x] == 'node':
                node_checker()
            elif sys.argv[x] == 'audio':
                audio_checker()
        else:
            print('USAGE: node|audio')