//
//  MainViewController.swift
//  FirstApp
//
//  Created by Valentina Vențel on 05/04/2019.
//  Copyright © 2019 Valentina Vențel. All rights reserved.
//

import UIKit

enum MenuItem: Int {
    case controlModes = 0, light, temperature
    
    var title: String {
        switch self {
        case .controlModes:
            return "Control modes"
        case .light:
            return "Light"
        case .temperature:
            return "Temperature"
        }
    }
    
    var segueID: String {
        switch self {
        case .controlModes:
            return "controlSegue"
        case .light:
            return "lightControlSegue"
        case .temperature:
            return "temperatureSegue"
        }
    }
    
}

class MainViewController: UIViewController {
    
    let optiuni: [MenuItem] = [.controlModes, .light, .temperature]
    
    @IBOutlet weak var tableView: UITableView!
    
    override func viewDidLoad() {
        super.viewDidLoad()

        // Do any additional setup after loading the view.
    
        tableView.dataSource = self
        tableView.delegate = self
        
        tableView.tableFooterView = UIView(frame: .zero)
        
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        
         title = NSLocalizedString("iHome", comment: "screen title")
    }
    
    override func viewWillDisappear(_ animated: Bool) {
        super.viewWillAppear(animated)
        
        title = ""
    }
    

    
    // MARK: - Navi

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        // Get the new view controller using segue.destination.
        // Pass the selected object to the new view controller.
    }
}

extension MainViewController: UITableViewDataSource {
    public func numberOfSections(in tableView: UITableView) -> Int {
            return 1
    }
    
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return optiuni.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let cell = tableView.dequeueReusableCell(withIdentifier: "myCell", for: indexPath)
        let menuItem: MenuItem = optiuni[indexPath.row]
        print(indexPath.row)
        cell.textLabel?.text = menuItem.title
        
        switch indexPath.row {
        case MenuItem.controlModes.rawValue: do {
            cell.imageView?.image = UIImage(named: "bluetooth-icon")
        }
        case MenuItem.light.rawValue: do {
            cell.imageView?.image = UIImage(named: "KrMzNwi")
            }
        case MenuItem.temperature.rawValue: do {
            cell.imageView?.image = UIImage(named: "kisspng-emoji-temperature-celsius-kelvin-fahrenheit-temperature-5acc9f626537b1.1590588015233595864146")
            }
        default:
            print("Error!")
        }
        
        return cell
    }
}

extension MainViewController: UITableViewDelegate {
    public func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        let menuItem = MenuItem(rawValue: indexPath.row)
        performSegue(withIdentifier:(menuItem?.segueID)! , sender: nil)
    }
    
    /*public func tableView(_ tableView: UITableView, heightForHeaderInSection section: Int) -> CGFloat {
        return 32.0
    }
    
     public func tableView(_ tableView: UITableView, viewForHeaderInSection section: Int) -> UIView? {
        let view = UIView(frame: CGRect(x: 0, y: 0, width: 100, height: 32))
        view.backgroundColor = UIColor.init(white: 0.3, alpha: 0.9)
        
        return view
    }*/


}


